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
    public class TestMovementStrategy {
    
    public Squadron squad;

    [SetUp]
    public void InitiateSquadron() {
        squad = new Squadron1();
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        GalagaBus.GetBus().Subscribe(GameEventType.MovementEvent, squad);
    }

    [TestCase(new NoMove())]
    [TestCase(new DownMove())]
    [TestCase(new ZigZig())]
    [TestCase(new RigidZigZag())]
    public void TestNoMoving(IMovementStrategy moveStrat) {
        squad.ChangeStrategy(moveStrat);
        int intialYPos = squad.Enemies[0].Shape.Position.Y;
        squad.Enemies.Iterate(enemy => {
            Assert.AreEqual(enemy.Shape.Position.Y, intialYPos);
        });
    }

    [TestCase(new DownMove())]
    [TestCase(new ZigZig())]
    [TestCase(new RigidZigZag())]
    public void TestMoveEnemy(IMovementStrategy moveStrat) {
        squad.Enemies.Iterate(enemy => {
            int intialPos = enemy.Shape.Position;
            moveStrat.MoveEnemy(enemy);
            Assert.AreNotEqual(enemy.Shape.Position, intialPos);
        });
    }
    [TestCase(new DownMove())]
    [TestCase(new ZigZig())]
    [TestCase(new RigidZigZag())]
    public void TestMoveEnemies(IMovementStrategy moveStrat) {
        int intialYPos = squad.Enemies[0].Shape.Position.Y;
        moveStrat.MoveEnemies(squad.Enemies);
        squad.Enemies.Iterate(enemy => {
            Assert.AreNotEqual(enemy.Shape.Position.Y, intialYPos);
        });
    }
}
}