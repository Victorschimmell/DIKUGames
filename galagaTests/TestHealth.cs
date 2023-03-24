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
    public class TestHealth {
    
        public Health testHealth;
        public int currentHealth;

        [SetUp]
        public void InitiateHealth() {
            testHealth = new Health(new Vec2F(0.1f, 0.1f), new Vec2F(0.1f, 0.1f));
        }

        [Test]
        public void TestStartHealth() {
            Assert.That(testHealth.CurrentHealth == 3);
        }

        [Test]
        public void TestLoseHealth() {
            Assert.LessOrEqual(testHealth.LoseHealth(1), 3);
        }

        [Test]
        public void TestAlwaysPositiveHealth() {
            for(int i = 0; i <= 10; i++){
                testHealth.LoseHealth(1);
            }
            Assert.GreaterOrEqual(testHealth.CurrentHealth, 0);
        }
    }
}