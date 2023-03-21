using Galaga;
using DIKUArcade.Events;
using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using DIKUArcade.GUI;
using System.Collections.Generic;
using Galaga.RandomSquadronCreater;
using Galaga.GameText;
using Galaga.Squadron;
using System.IO;
using Galaga.GalagaStates;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using Galaga.GalagaStates;

namespace GalagaTests {
    [TestFixture]
    public class StateMachineTesting {
        private StateMachine stateMachine;
    [SetUp]
    public void InitiateStateMachine() {
        Window.CreateOpenGLContext();
        
        /*Here you should:
        (1) Initialize a GalagaBus with proper GameEventTypes
        (2) Instantiate the StateMachine
        (3) Subscribe the GalagaBus to proper GameEventTypes
        and GameEventProcessors*/
        stateMachine = new StateMachine();
        GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
    }

    [Test]
    public void TestInitialState() {
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
    }

    [Test]
    public void TestEventGamePaused() {
        GalagaBus.GetBus().RegisterEvent(
            new GameEvent{
                EventType = GameEventType.GameStateEvent,
                Message = "CHANGE_STATE",
                StringArg1 = "GAME_PAUSED"
            }
        );
        GalagaBus.GetBus().ProcessEventsSequentially();
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GamePaused>());
    }
    }
}