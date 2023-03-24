using Galaga;
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
using Galaga.MovementStrategy;

namespace GalagaTests {
    [TestFixture]
    public class TestSquadron {
    
        public ISquadron squad;

        [SetUp]
        public void InitiatePlayer() {
            squad = new Squadron1(ImageStride.CreateStrides
                    (4, Path.Combine("Assets", "Images", "BlueMonster.png")), ImageStride.CreateStrides(2, Path.Combine("Assets",
                    "Images", "GreenMonster.png")));
            squad.ChangeStrategy(new DownMove());
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            GalagaBus.GetBus().Subscribe(GameEventType.MovementEvent, squad);
        }

        [Test]
        public void TestNoMove() {
            float intialYPos = 0f;
            squad.Enemies.Iterate(enemy => {
                intialYPos = enemy.Shape.Position.Y;
            });
            GalagaBus.GetBus().ProcessEventsSequentially();
            squad.Enemies.Iterate(enemy => {
                Assert.That(enemy.Shape.Position.Y == intialYPos);
            });
        }

        [Test]
        public void TestProcessEvent() {
            float intialYPos = 0f;
            squad.Enemies.Iterate(enemy => {
                intialYPos = enemy.Shape.Position.Y;
            });
            GalagaBus.GetBus().RegisterEvent(new GameEvent{
                EventType = GameEventType.MovementEvent,
                Message = "MoveAll"
            });
            GalagaBus.GetBus().ProcessEventsSequentially();
            squad.Enemies.Iterate(enemy => {
                Assert.That(enemy.Shape.Position.Y == intialYPos -0.001f);
            });
        }
        [Test]
        public void TestChangeSpeed() {
            squad.ChangeSpeed(new Vec2F(0f, -0.001f));
            squad.Enemies.Iterate(enemy => {
                Assert.That(enemy.Shape.AsDynamicShape().Direction.ToString() == (new Vec2F(0f, -0.002f)).ToString());
            });
        }
        [Test]
        public void TestChangeStrategy() {
            IMovementStrategy strat = new ZigZag();
            squad.ChangeStrategy(strat);
            Assert.That(squad.Strategy == strat);
        }
    }
}
