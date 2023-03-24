using Galaga;
using DIKUArcade.Events;
using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.GUI;
using System.Collections.Generic;
using Galaga.RandomSquadronCreater;
using Galaga.GameText;
using Galaga.Squadron;
using System.IO;
using Galaga.GalagaStates;
using DIKUArcade;

namespace GalagaTests {
    [TestFixture]
    public class StateMachineTesting {
        private StateMachine stateMachine;
        
    [SetUp]
    public void InitiateStateMachine() {
        //System.IO.Directory.SetCurrentDirectory(Galaga.Game.GetProjectPath());
        Window.CreateOpenGLContext();
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

    [Test]
    public void TestEventGameRunning() {
        GalagaBus.GetBus().RegisterEvent(
            new GameEvent{
                EventType = GameEventType.GameStateEvent,
                Message = "CHANGE_STATE",
                StringArg1 = "GAME_RUNNING"
            }
        );
        GalagaBus.GetBus().ProcessEventsSequentially();
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameRunning>());
    }
    [Test]
    public void TestEventGameOver() {
        GalagaBus.GetBus().RegisterEvent(
            new GameEvent{
                EventType = GameEventType.GameStateEvent,
                Message = "CHANGE_STATE",
                StringArg1 = "GAME_OVER"
            }
        );
        GalagaBus.GetBus().ProcessEventsSequentially();
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameOver>());
    }
    [Test]
    public void TestEventMainMenu() {
        GalagaBus.GetBus().RegisterEvent(
            new GameEvent{
                EventType = GameEventType.GameStateEvent,
                Message = "CHANGE_STATE",
                StringArg1 = "MAIN_MENU"
            }
        );
        GalagaBus.GetBus().ProcessEventsSequentially();
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
    }
    }
}