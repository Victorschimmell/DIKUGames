using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using DIKUArcade.GUI;
using System.Collections.Generic;
using System.IO;

namespace Breakout.States{
    public class GameRunning : IGameState {
        private static GameRunning instance = null;
        private Player player;
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
            }
        }

        /*private void KeyRelease(KeyboardKey key) {
            GameEvent MoveStopLeftRight = new GameEvent{
                EventType = GameEventType.MovementEvent,
                Message = "MoveStopLeftRight"};
            switch (key) {
                case KeyboardKey.Left:
                    BreakoutBus.GetBus().RegisterEvent(MoveStopLeftRight);
                    break;
                case KeyboardKey.Right:
                    BreakoutBus.GetBus().RegisterEvent(MoveStopLeftRight);
                    break;
            }
        }*/

        void IGameState.HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            switch (action) {
                case KeyboardAction.KeyPress:
                    KeyPress(key);
                    break;
                /*case KeyboardAction.KeyRelease:
                    KeyRelease(key);
                    break;*/
            }
        }

        public void RenderState(){
            player.Render();
        }

        public void ResetState(){
            player.Shape.SetPosition(new Vec2F(0.45f, 0.1f));
            BreakoutBus.GetBus().RegisterEvent(
                new GameEvent{
                    EventType = GameEventType.MovementEvent,
                    Message = "MoveStopAll"
                });
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
                new DynamicShape(new Vec2F(0.4f, 0.1f), new Vec2F(0.2f, 0.05f)),
                new Image(Path.Combine("Assets", "Images", "player.png")));
            // EventBus
            BreakoutBus.GetBus().Subscribe(GameEventType.MovementEvent, player);
        }
    }
}