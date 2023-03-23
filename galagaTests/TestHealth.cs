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
    public class TestHealth {
    
        public Health testHealth;

        [SetUp]
        public void InitiateHealth() {
            testHealth = new Health(new Vec2F(0.1f, 0.1f), new Vec2F(0.1f, 0.1f));
            testHealth.CreateOpenGLContext();
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            GalagaBus.GetBus().Subscribe(GameEventType.GraphicsEvent, testHealth);
        }

        [Test]
        public void TestStartHealth() {
            Assert.AreEqual(testHealth.CurrentHealth, 3);
        }

        [Test]
        public void TestLoseHealth() {
            Assert.LessThan(testHealth.LoseHealth(), 3);
        }

        [Test]
        public void TestAlwaysPositiveHealth() {
            for(int i = 0; i <= 10; i++){
                testHealth.LoseHealth();
            }
            Assert.GreaterEqual(testHealth.LoseHealth(), 0);
        }
    }
}