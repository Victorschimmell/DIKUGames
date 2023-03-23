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

namespace GalagaTests {
    [TestFixture]
    public class TestSquadron {
    
    public Squadron squad;

    [SetUp]
    public void InitiatePlayer() {
        squad = new Squadron1();
        squad.ChangeStrategy(new DownMove());
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        GalagaBus.GetBus().Subscribe(GameEventType.MovementEvent, squad);
    }

    [Test]
    public void TestNoMove() {
        int intialYPos = squad.Enemies[0].Shape.Position.Y;
        GalagaBus.GetBus().ProcessEventsSequentially();
        squad.Enemies.Iterate(enemy => {
            Assert.AreEqual(enemy.Shape.Position.Y, intialYPos);
        });
    }

    [Test]
    public void TestProcessEvent() {
        int intialYPos = squad.Enemies[0].Shape.Position.Y;
        GalagaBus.GetBus().RegisterEvent(new GameEvent{
            EventType = GameEventType.MovementEvent,
            Message = "MoveAll"
        });
        GalagaBus.GetBus().ProcessEventsSequentially();
        squad.Enemies.Iterate(enemy => {
            Assert.AreEqual(enemy.Shape.Position.Y, intialYPos -0.001f);
        });
    }
    [Test]
    public void TestChangeSpeed() {
        Vec2F intialYPos = squad.Enemies[0].Shape.Direction;
        squad.ChangeSpeed(new Vec2F(0f,2f));
        squad.Enemies.Iterate(enemy => {
            Assert.AreEqual(enemy.Shape.Direction, new Vec2F(0f,2f));
        });
    }
    [Test]
    public void TestChangeStrategy() {
        IMovementStrategy strat = new ZigZig();
        squad.ChangeStrategy(strat);
        Assert.AreEqual(squad.Strategy, strat);
    }

}
}