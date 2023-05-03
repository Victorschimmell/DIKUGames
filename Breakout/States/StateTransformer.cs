namespace Breakout.States {
    public class StateTransformer {
        public static GameStateType TransformStringToState(string state) {
            switch(state) {
                case "GAME_RUNNING":
                    return GameStateType.GameRunning;
                default:
                    throw new System.ArgumentException("Invalid state-input!");
            }
        }

        public static string TransformStateToString(GameStateType type) {
            switch(type) {
                case GameStateType.GameRunning:
                    return "GAME_RUNNING";
                default:
                    throw new System.ArgumentException("Invalid type-input!");
            }
        }
    }
}