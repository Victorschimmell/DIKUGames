using Breakout;
using Breakout.Blocks;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Collections.Generic;
using System.IO;

namespace BreakoutTests.EntityTests {
    [TestFixture]
    public class TestBlock {
        private BlueBlock blueBlock;

        [SetUp]
        public void InitiateBlock(){
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            blueBlock = new BlueBlock(new DynamicShape(new Vec2F(0.45f, 0.8f),
            new Vec2F(0.08f, 0.025f)));
        }

        [Test]
        public void TestHealth() {
            Assert.That(blueBlock.GetHealth() == 15);
        }

        [Test]
        public void TestDamaged() {
            Assert.That(blueBlock.IsDamaged() == false);
            for (int i = 0; i <= 8; i++) {
                blueBlock.TakeDamage();
            }
            Assert.That(blueBlock.IsDamaged() == true);
        }

        [Test]
        public void TestDeadBlock() {
            Assert.That(blueBlock.IsDeleted() == false);
            for(int i = 0; i < 15; i++) {
                blueBlock.TakeDamage();
            }
            Assert.That(blueBlock.IsDeleted() == true);
        }
    }
}