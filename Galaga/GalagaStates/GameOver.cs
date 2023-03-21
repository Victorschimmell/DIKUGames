using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Events;
using System.IO;

namespace Galaga.GalagaStates {
    public class GameOver : IGameState {
        private static GameOver instance = null;
        private Entity backGroundImage;
        private Text gameoverText;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;
        public static GameOver GetInstance() {
            if (GameOver.instance == null) {
                GameOver.instance = new GameOver();
                GameOver.instance.InitializeGameState();
            }
            GameOver.instance.ResetState();
            return GameOver.instance;
        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            switch (action) {
                case KeyboardAction.KeyPress:
                    KeyPress(key);
                    break;
                case KeyboardAction.KeyRelease:
                    break;
            }
        }

        public void KeyPress(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Up:
                    activeMenuButton = (activeMenuButton - 1) % maxMenuButtons;
                    break;
                case KeyboardKey.Down:
                    activeMenuButton = (activeMenuButton + 1) % maxMenuButtons;
                    break;
                case KeyboardKey.Enter:
                    switch (menuButtons[activeMenuButton].ToString()) {
                        case "Quit":
                            GalagaBus.GetBus().RegisterEvent(
                                new GameEvent{
                                    EventType = GameEventType.WindowEvent,
                                    Message = "CloseWindow",
                                });
                            break;
                        default:
                            GalagaBus.GetBus().RegisterEvent(
                                new GameEvent{
                                    EventType = GameEventType.GameStateEvent,
                                    Message = "CHANGE_STATE",
                                    StringArg1 = "GAME_RUNNING"
                                }
                            );
                            break;
                    }
                    break;
            }
        }

        public void RenderState() {
            backGroundImage.RenderEntity();
            foreach (Text button in menuButtons) {
                button.RenderText();
            }
            gameoverText.RenderText();
        }

        public void ResetState() {
            activeMenuButton = 0;
        }

        public void UpdateState() {
            menuButtons[activeMenuButton].SetColor(new Vec3F(1f, 0f, 0f));
        }

        private void InitializeGameState() {
            backGroundImage = new Entity(new DynamicShape(new Vec2F(0f, 0f), new Vec2F(1f, 1f)),
                new Image(Path.Combine("Assets", "Images", "TitleImage.png")));
            menuButtons = new Text[2];
            menuButtons[0] = new Text("Try Again", new Vec2F(0.2f, 0f), new Vec2F(0.8f, 0.8f));
            menuButtons[1] = new Text("Quit", new Vec2F(0.0f, 0f), new Vec2F(0.8f, 0.8f));
            activeMenuButton = 0;
            maxMenuButtons = menuButtons.Length;
            menuButtons[activeMenuButton].SetColor(new Vec3F(1f, 0f, 0f));
            gameoverText = new Text("Game Over", new Vec2F(0.0f, 0.2f), new Vec2F(0.8f, 0.8f));
            gameoverText.SetFontSize(100);
            gameoverText.SetColor(new Vec3F(1f, 0f, 0f));
        }
    }
}