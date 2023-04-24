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
    }
}