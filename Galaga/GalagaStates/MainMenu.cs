using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Events;
using System.IO;

namespace Galaga.GalagaStates {
    public class MainMenu : IGameState {
        private static MainMenu instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;
        public static MainMenu GetInstance() {
            if (MainMenu.instance == null) {
                MainMenu.instance = new MainMenu();
                MainMenu.instance.InitializeGameState();
            }
            MainMenu.instance.ResetState();
            return MainMenu.instance;
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
                    activeMenuButton = (System.Math.Max(activeMenuButton - 1, 0));
                    break;
                case KeyboardKey.Down:
                    activeMenuButton = (System.Math.Min(activeMenuButton + 1, maxMenuButtons - 1));
                    break;
                case KeyboardKey.Enter:
                    switch (activeMenuButton) {
                        case 0:
                            GalagaBus.GetBus().RegisterEvent(
                                new GameEvent{
                                    EventType = GameEventType.GameStateEvent,
                                    Message = "CHANGE_STATE",
                                    StringArg1 = "GAME_RUNNING"
                                }
                            );
                            break;
                        case 1:
                            GalagaBus.GetBus().RegisterEvent(
                                new GameEvent{
                                    EventType = GameEventType.WindowEvent,
                                    Message = "CloseWindow",
                                });
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
        }

        public void ResetState() {
            activeMenuButton = 0;
        }

        public void UpdateState() {
            menuButtons[activeMenuButton].SetColor(new Vec3F(1f, 1f, 1f));
            menuButtons[maxMenuButtons - 1 - activeMenuButton].SetColor(new Vec3F(0.2f, 0.2f, 0.2f));
        }

        private void InitializeGameState() {
            backGroundImage = new Entity(new DynamicShape(new Vec2F(0f, 0f), new Vec2F(1f, 1f)),
                new Image(Path.Combine("Assets", "Images", "TitleImage.png")));
            menuButtons = new Text[2];
            menuButtons[0] = new Text("New Game", new Vec2F(0.2f, 0f), new Vec2F(0.8f, 0.8f));
            menuButtons[1] = new Text("Quit", new Vec2F(0.2f, -0.2f), new Vec2F(0.8f, 0.8f));
            activeMenuButton = 0;
            maxMenuButtons = menuButtons.Length;
        }
    }
}