using Breakout;
using Breakout.LevelLoading;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Collections.Generic;
using System.IO;

namespace BreakoutTests.LevelLoadingTests {
    [TestFixture]
    public class TestLoading {
        private ASCIIReader reader;
        private MapLoader loader;

        [SetUp]
        public void InitiateLoader() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            reader = new ASCIIReader(Path.Combine("Assets", "Levels", "test-level.txt"));
            loader = new MapLoader(reader);
            loader.LoadBlocks();
        }

        [Test]
        public void TestBlockCount() {
            Assert.That(loader.Blocks.CountEntities() == 8);
        }

        [Test]
        public void TestBlockInsideWindow() {
            loader.Blocks.Iterate(block => {
                Vec2F pos = block.GetPosition();
                Assert.That(pos.Y >= 0 && pos.Y <= 1);
                Assert.That(pos.X >= 0 && pos.X <= 1);
            });
        }

        [Test]
        public void TestCorrectBlockType() {
            // Test uses the values of the block types, which are all different to determine
            // if a block is the same.
            // Type "0" = 20, type "Y" = 200
            // Test map is following:
            // ------------
            // 000Y----Y000
            // ------------
            List<int> targetBlockTypes = new List<int>{20, 20, 20, 200, 200, 20, 20, 20};
            List<int> blockTypes = new List<int>();
            loader.Blocks.Iterate(block => {
                blockTypes.Add(block.Value);
            });
            for (int i = 0; i < blockTypes.Count; i++) {
                Assert.That(blockTypes[i] == targetBlockTypes[i]);
            }
        }

        [Test]
        public void TestBlockCorrectPosition() {
            float i = 0;
            loader.Blocks.Iterate(block => {
                Vec2F expectedPos = new Vec2F((0.5f - (12f / 2f * 0.08f) + (i * 0.08f)), 
                (1f - 0.025f * 2f));
                Assert.That(expectedPos.ToString() == block.GetPosition().ToString());
                // Skips the row "----" with empty positions in the file
                if(i == 3) {
                    i = 8;
                } else {
                    i++;
                }
            });
        }
    }


}