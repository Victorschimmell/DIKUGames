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

namespace GalagaTests {
    [TestFixture]
    public class TestEnemy {
        public List<Image> image1;
        public List<Image> image2;
        public Enemy testEnemy;

        [SetUp]
        public void InitiateEnemy() {
            image1 = ImageStride.CreateStrides
                (4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            image2 = ImageStride.CreateStrides(2, Path.Combine("Assets",
                "Images", "GreenMonster.png"));
            testEnemy = new Enemy(
                new DynamicShape(new Vec2F(0.1f, 1f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, image1), new ImageStride(80, image2));
        }

        [Test]
        public void TestStartPosition() {
            Assert.That(testEnemy.Shape.Position.ToString() == (new Vec2F(0.1f, 1f)).ToString());
        }

        [Test]
        public void TestHealth() {
            Assert.That(testEnemy.HitPoints == 3);
        }

        [Test]
        public void TestTakeDamage() {
            testEnemy.TakeDamage();
            Assert.That(testEnemy.HitPoints == 2);
        }

        [Test]
        public void TestEnrage() {
            Assert.That(testEnemy.IsEnraged == false);
            testEnemy.TakeDamage();
            Assert.That(testEnemy.IsEnraged == true);
        }

        [Test]
        public void TestEnemyDie() {
            Assert.That(testEnemy.IsDeleted() == false);
            for(int i = 0; i < 3; i++) {
                testEnemy.TakeDamage();
            }
            Assert.That(testEnemy.IsDeleted() == true);
        }
    }
}