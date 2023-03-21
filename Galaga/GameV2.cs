using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;
using DIKUArcade.Physics;
using Galaga.Squadron;
using Galaga.RandomSquadronCreater;
using Galaga.GameText;
using Galaga.GalagaStates;

namespace Galaga;
public class GameV2 : DIKUGame, IGameEventProcessor {
    private WindowArgs windowArgs;
    private StateMachine stateMachine;
    public GameV2(WindowArgs windowArgs) : base(windowArgs) {
        this.windowArgs = windowArgs;
        window.SetKeyEventHandler(KeyHandler);
        GalagaBus.GetBus().Subscribe(GameEventType.WindowEvent, this);
        stateMachine = new StateMachine();
    }
    public override void Render() {
       stateMachine.ActiveState.RenderState();
    }
    public override void Update() {
        stateMachine.ActiveState.UpdateState();
        window.PollEvents();
        GalagaBus.GetBus().ProcessEventsSequentially();
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