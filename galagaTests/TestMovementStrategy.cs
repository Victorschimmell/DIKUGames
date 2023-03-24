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
using Galaga.MovementStrategy;
using System.IO;

namespace GalagaTests {
    [TestFixture]
    public class TestMovementStrategy {
    
        public ISquadron squad;
        public IMovementStrategy strat;

        public List<Image> image1;
        public List<Image> image2;

        [SetUp]
        public void InitiateSquadron() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            strat = new NoMove();
            image1 = ImageStride.CreateStrides
                (4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            image2 = ImageStride.CreateStrides(2, Path.Combine("Assets",
                "Images", "GreenMonster.png"));
            squad = new Squadron1(image1, image2);
            GalagaBus.GetBus().Subscribe(GameEventType.MovementEvent, squad);
        }

        // Single Enemy
        [Test]
        public void TestMoveEnemyNoMove() {
            strat = new NoMove();
            squad.Enemies.Iterate(enemy => {
                Vec2F intialPos = enemy.Shape.Position;
                strat.MoveEnemy(enemy);
                Assert.That(enemy.Shape.Position.ToString() == intialPos.ToString());
            });
        }

        [Test]
        public void TestMoveEnemyDownMove() {
            strat = new DownMove();
            squad.Enemies.Iterate(enemy => {
                Vec2F intialPos = enemy.Shape.Position;
                strat.MoveEnemy(enemy);
                Assert.That(enemy.Shape.Position.ToString() == (intialPos + new Vec2F(0.0f, -0.001f)).ToString());
            });
        }

        [Test]
        public void TestMoveEnemyZigZag() {
            strat = new ZigZag();
            squad.Enemies.Iterate(enemy => {
                float sinCalc = (float) Math.Sin(1.5+(2f * Math.PI * (enemy.StartPos.Y - enemy.Shape.Position.Y))
                    / 0.045f);
                float intialPos = enemy.Shape.Position.X;
                strat.MoveEnemy(enemy);
                Assert.That(enemy.Shape.Position.X.ToString() == (intialPos + 0.01f * sinCalc).ToString());
            });
        }

        [Test]
        public void TestMoveEnemyRigidZigZag() {
            strat = new RigidZigZag();
            squad.Enemies.Iterate(enemy => {
                float sinCalc = (float) Math.Sin(0.5+(2f * Math.PI * (enemy.StartPos.Y - enemy.Shape.Position.Y))
                    / 0.09f);
                if (sinCalc <= 0) {
                    sinCalc = -1f;
                }
                else {
                    sinCalc = 1f;
                }
                float intialPos = enemy.Shape.Position.X;
                strat.MoveEnemy(enemy);
                Assert.That(enemy.Shape.Position.X.ToString() == (intialPos + 0.004f * sinCalc).ToString());
            });
        }

        // Multiple Enemies        
        [Test]
        public void TestMoveEnemiesNoMove() {
            strat = new NoMove();
            float YPos = 1f;
            squad.Enemies.Iterate(enemy => {
                Assert.That(enemy.Shape.Position.Y <= YPos);
            });
            strat.MoveEnemies(squad.Enemies);
            squad.Enemies.Iterate(enemy => {
                Assert.That(enemy.Shape.Position.Y <= YPos);
            });
        }

        [Test]
        public void TestMoveEnemiesDownMove() {
            strat = new DownMove();
            float YPos = 1f;
            squad.Enemies.Iterate(enemy => {
                Assert.That(enemy.Shape.Position.Y <= YPos);
            });
            strat.MoveEnemies(squad.Enemies);
            squad.Enemies.Iterate(enemy => {
                Assert.That(enemy.Shape.Position.Y <= YPos);
            });
        }

        [Test]
        public void TestMoveEnemiesZigZag() {
            strat = new ZigZag();
            float YPos = 1f;
            squad.Enemies.Iterate(enemy => {
                Assert.That(enemy.Shape.Position.Y <= YPos);
            });
            strat.MoveEnemies(squad.Enemies);
            squad.Enemies.Iterate(enemy => {
                Assert.That(enemy.Shape.Position.Y <= YPos);
            });
        }

        [Test]
        public void TestMoveEnemiesRigidZigZag() {
            strat = new RigidZigZag();
            float YPos = 1f;
            squad.Enemies.Iterate(enemy => {
                Assert.That(enemy.Shape.Position.Y <= YPos);
            });
            strat.MoveEnemies(squad.Enemies);
            squad.Enemies.Iterate(enemy => {
                Assert.That(enemy.Shape.Position.Y <= YPos);
            });
        }
    }
}