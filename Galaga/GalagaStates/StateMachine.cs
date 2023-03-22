using DIKUArcade.Events;
using DIKUArcade.State;
using DIKUArcade.Events.Generic;

namespace Galaga.GalagaStates {
    public class StateMachine : IGameEventProcessor {
        public IGameState ActiveState { get; private set; }
        public StateMachine() {
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            ActiveState = MainMenu.GetInstance();
        }

        private void SwitchState(GameStateType stateType) {
            switch (stateType) {
                case GameStateType.GameRunning:
                    ActiveState = GameRunning.GetInstance();
                    break;
                case GameStateType.GamePaused:
                    ActiveState = GamePaused.GetInstance();
                    break;
                case GameStateType.MainMenu:
                    ActiveState = MainMenu.GetInstance();
                    GameRunning.GetInstance().ResetState();
                    break;
                case GameStateType.GameOver:
                    GameOver.GetInstance().SetRounds(GameRunning.GetInstance().RoundCounter.Clone());
                    ActiveState = GameOver.GetInstance();
                    GameRunning.GetInstance().ResetState();
                    break;
            }
        }

        void IGameEventProcessor.ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.GameStateEvent) {
                switch (gameEvent.Message) {
                    case "CHANGE_STATE":
                        SwitchState(StateTransformer.TransformStringToState(gameEvent.StringArg1));
                        break;
                }
            }
        }
    }
}