using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using Breakout.States;

namespace Breakout;
public class Game : DIKUGame, IGameEventProcessor {
    private WindowArgs windowArgs;
    private StateMachine stateMachine;
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        this.windowArgs = windowArgs;
        window.SetKeyEventHandler(KeyHandler);
        BreakoutBus.GetBus().Subscribe(GameEventType.WindowEvent, this);
        stateMachine = new StateMachine();
    }

    public override void Render() {
       stateMachine.ActiveState.RenderState();
    }

    public override void Update() {
        window.PollEvents();
        BreakoutBus.GetBus().ProcessEventsSequentially();
        stateMachine.ActiveState.UpdateState();
    }

    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        stateMachine.ActiveState.HandleKeyEvent(action, key);
    }

    void IGameEventProcessor.ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.WindowEvent) {
            switch (gameEvent.Message) {
            case "CloseWindow":
                window.CloseWindow();
                break;
            default:
                break;
        }};
    }
}