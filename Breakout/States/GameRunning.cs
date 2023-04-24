using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using DIKUArcade.GUI;
using Breakout;
using Breakout.Blocks;
using System.Collections.Generic;
using Breakout.LevelLoading;

namespace Breakout.States {
    public class GameRunning : IGameState {
        private static GameRunning instance = null;
        private Player player;
        private MapLoader fileLoader;
        public static GameRunning GetInstance() {
            if (GameRunning.instance == null) {
                GameRunning.instance = new GameRunning();
                GameRunning.instance.InitializeGameState();
            }
            return GameRunning.instance;
        }

        private void KeyPress(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Left:
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                                    EventType = GameEventType.MovementEvent,
                                    Message = "MoveLeft"
                                });
                    break;
                case KeyboardKey.A:
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                                    EventType = GameEventType.MovementEvent,
                                    Message = "MoveLeft"
                                });
                    break;
                case KeyboardKey.Right:
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                                    EventType = GameEventType.MovementEvent,
                                    Message = "MoveRight"
                                });
                    break;
                case KeyboardKey.D:
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                                    EventType = GameEventType.MovementEvent,
                                    Message = "MoveRight"
                                });
                    break;
                case KeyboardKey.Escape:
                    BreakoutBus.GetBus().RegisterEvent(
                                new GameEvent{
                                    EventType = GameEventType.WindowEvent,
                                    Message = "CloseWindow",
                                }
                            );
                        break;
                // TEMPORARY EVENT TO TEST BREAKAGE OF BLOCKS //
                case KeyboardKey.F:
                    fileLoader.Blocks.Iterate(block => {
                        block.TakeDamage();
                        if (block.IsDeleted()) {
                            player.AddPoints(block.Value);
                        }
                    });
                    break;
                ////////////////////////////////////////////////
            }
        }

        void IGameState.HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            switch (action) {
                case KeyboardAction.KeyPress:
                    KeyPress(key);
                    break;
            }
        }

        public void RenderState(){
            player.Render();
            fileLoader.Blocks.RenderEntities();
        }

        public void ResetState(){
            player.Shape.SetPosition(new Vec2F(0.45f, 0.1f));
            BreakoutBus.GetBus().RegisterEvent(
                new GameEvent{
                    EventType = GameEventType.MovementEvent,
                    Message = "MoveStopAll"
                });
            fileLoader = null;
        }

        public void UpdateState(){
            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                EventType = GameEventType.MovementEvent,
                Message = "MoveAll"
            });
        }

        private void InitializeGameState() {
            // Player
            player = new Player(
                new DynamicShape(new Vec2F(0.425f, 0.1f), new Vec2F(0.15f, 0.02f)),
                new Image(Path.Combine("Assets", "Images", "player.png")));
            // EventBus
            BreakoutBus.GetBus().Subscribe(GameEventType.MovementEvent, player);
            // Map
            SetMap(Path.Combine("Assets", "Levels", "test.txt"));
        }

        private void SetMap(string mapName) {
            fileLoader = new MapLoader(new ASCIIReader(mapName));
            fileLoader.LoadBlocks();
        }
    }
}