using DIKUArcade.Events;
using DIKUArcade.State;
using DIKUArcade.Events.Generic;

namespace Galaga.GalagaStates {
    public class StateMachine : IGameEventProcessor<object> {
    public IGameState ActiveState { get; private set; }
    public StateMachine() {
        GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
        ActiveState = MainMenu.GetInstance();
    }

    private void SwitchState(GameStateType stateType) {
        switch (stateType) { ... }

    void IGameEventProcessor<object>.ProcessEvent(GameEvent<object> gameEvent) {
            throw new System.NotImplementedException();
        }
    }
    }
}