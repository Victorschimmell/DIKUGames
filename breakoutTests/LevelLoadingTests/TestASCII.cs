using Breakout;
using Breakout.LevelLoading;
using System.Collections.Generic;
using System.IO;

namespace BreakoutTests.LevelLoadingTests {
    [TestFixture]
    public class TestASCII {
        private ASCIIReader reader;

        [SetUp]
        public void InitiateReader() {
            reader = new ASCIIReader(Path.Combine("Assets", "Levels", "test-level.txt"));
        }

        [Test]
        public void TestMap() {
            //List<string> map = reader.GetMap();
            List<string> targetMap = new List<string>();
            targetMap.Add("------------");
            targetMap.Add("000Y----Y000");
            targetMap.Add("------------");
            int currentNum = 0;
            foreach (string line in reader.map) {
                Assert.That(line == targetMap[currentNum]);
                currentNum++;
            }
        }
    }
}