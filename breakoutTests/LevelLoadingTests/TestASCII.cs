using Breakout;
using Breakout.LevelLoading;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using System.IO;

namespace BreakoutTests.LevelLoadingTests {
    [TestFixture]
    public class TestASCII {
        private ASCIIReader reader;
        private ASCIIReader brokenFileReader;
        private ASCIIReader emptyReader;

        [SetUp]
        public void InitiateReader() {
            reader = new ASCIIReader(Path.Combine("Assets", "Levels", "test-level.txt"));
            brokenFileReader = new ASCIIReader(Path.Combine(
                "Assets", "Levels", "broken-test-level.txt"));
        }

        /* File looks like this:
        Map:
        ------------
        000Y----Y000
        ------------
        Map/

        Meta:
        Name: TEST LEVEL
        Meta/

        Legend:
        0) orange-block.png
        Y) brown-block.png
        Legend/
        */

        /* Broken file looks like this:
        Map:
        ------------
        000Y----Y000
        ------------
        Map

        Met
        Name: TEST LEVEL
        Meta/

        Legend:
        0 orange-block.png
        Y) brown-block.png
        Legend/
        */

        [Test]
        public void TestNotNull() {
            Assert.NotNull(reader.GetMeta());
            Assert.NotNull(reader.GetMap());
            Assert.NotNull(reader.GetLegend());
        }

        [Test]
        public void TestMap() {
            List<string> targetMap = new List<string>();
            targetMap.Add("------------");
            targetMap.Add("000Y----Y000");
            targetMap.Add("------------");
            List<string> map = reader.GetMap();
            for (int i = 0; i < map.Count; i++) {
                Assert.That(map[i] == targetMap[i]);
            }
        }

        [Test]
        public void TestBrokenMap() {
            List<string> brokenMap = brokenFileReader.GetMap();
            Assert.NotNull(brokenMap);
        }

        [Test]
        public void TestMeta() {
            Dictionary<string, string> targetMeta = new Dictionary<string, string>();
            targetMeta.Add("Name", "TEST LEVEL");
            Dictionary<string, string> meta = reader.GetMeta();
            for (int i = 0; i < meta.Count; i++) {
                Assert.That(meta.ElementAt(i).Key == targetMeta.ElementAt(i).Key);
                Assert.That(meta.ElementAt(i).Value == targetMeta.ElementAt(i).Value);
            }
        }

        [Test]
        public void TestBrokenMeta() {
            Dictionary<string, string> brokenMeta = brokenFileReader.GetMeta();
            Assert.NotNull(brokenMeta);
        }

        [Test]
        public void TestLegend() {
            Dictionary<string, string> targetLegend = new Dictionary<string, string>();
            targetLegend.Add("0", "orange-block.png");
            targetLegend.Add("Y", "brown-block.png");
            Dictionary<string, string> legend = reader.GetLegend();
            for (int i = 0; i < legend.Count; i++) {
                Assert.That(legend.ElementAt(i).Key == targetLegend.ElementAt(i).Key);
                Assert.That(legend.ElementAt(i).Value == targetLegend.ElementAt(i).Value);
            }
        }

        [Test]
        public void TestBrokenLegend() {
            Dictionary<string, string> targetLegend = new Dictionary<string, string>();
            // The broken file line "0 orange-block.png" without the ")" is ignored
            targetLegend.Add("Y", "brown-block.png");
            Dictionary<string, string> brokenLegend = brokenFileReader.GetLegend();
            for (int i = 0; i < brokenLegend.Count; i++) {
                Assert.That(brokenLegend.ElementAt(i).Key == targetLegend.ElementAt(i).Key);
                Assert.That(brokenLegend.ElementAt(i).Value == targetLegend.ElementAt(i).Value);
            }
        }
    }
}