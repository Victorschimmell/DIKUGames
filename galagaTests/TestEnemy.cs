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
    public class TestEnemy {
    
    public Enemy testEnemy;

    [SetUp]
    public void InitiateEnemy() {
        testEnemy = new Enemy(
            new DynamicShape((0.1f, 1f), new Vec2F(0.1f, 0.1f)),
            new ImageStride(80, enemyStride), new ImageStride(80, alternativeEnemyStride));
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        GalagaBus.GetBus().Subscribe(GameEventType.MovementEvent, testEnemy);
    }

    [Test]
    public void TestStartPosition() {
        Assert.AreEqual(testEnemy.GetShape().Position, new Vec2F(0.1f, 0.1f));
    }

    [Test]
    public void TestHealth() {
        Assert.AreEqual(testEnemy.HitPoints, 3);
    }

    [Test]
    public void TestTakeDamage() {
        testEnemy.TakeDamage();
        Assert.AreEqual(testEnemy.HitPoints, 2);
    }

    [Test]
    public void TestEnrage() {
        Assert.AreNotEqual(testEnemy.IsEnraged, true);
        testEnemy.TakeDamage();
        Assert.AreEqual(testEnemy.IsEnraged, true);
    }

    [Test]
    public void TestEnemyDie() {
        Assert.AreNotEqual(testEnemy.IsDeleted(), true);
        for(int i = 0; i < 3; i++) {
            testEnemy.TakeDamage();
        }
        Assert.AreEqual(testEnemy.IsDeleted(), true);
    }
}
}