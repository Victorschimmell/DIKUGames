namespace Galaga.GalagaStates {
public enum GameStateType {
    GameRunning,
    GamePaused,
    MainMenu,
}

public class StateTransformer {
    public static GameStateType TransformStringToState(string state) {
        switch(state) {
            case "GAME_RUNNING":
                return GameStateType.GameRunning;
            case "GAME_PAUSED":
                return GameStateType.GamePaused;
            case "MAIN_MENU":
                return GameStateType.MainMenu;
            default:
                throw new System.ArgumentException("Invalid state-input!");
        }
    }

    public static string TransformStringToState(GameStateType type) {
        switch(type) {
            case GameStateType.GameRunning:
                return "GAME_RUNNING";
            case GameStateType.GamePaused:
                return "GAME_PAUSED";
            case GameStateType.MainMenu:
                return "MAIN_MENU";
            default:
                throw new System.ArgumentException("Invalid type-input!");
        }
    }
}
}