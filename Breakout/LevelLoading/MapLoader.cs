using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using Breakout.Blocks;

namespace Breakout.LevelLoading {
    public class MapLoader {
        public EntityContainer<Block> Blocks {get;}
        private List<string> map;
        private List<string> meta;
        private Dictionary<string, string> legend;
        public MapLoader(ASCIIReader reader) {
            map = reader.GetMap();
            meta = reader.GetMeta();
            legend = reader.GetLegend();
            Blocks = new EntityContainer<Block>();
        }

        public void LoadBlocks() {
            int currentHeight = 0;
            int mapHeight = map.Count();
            foreach (string line in map) {
                currentHeight++;
                char[] individualSymbols = line.ToCharArray();
                float length = (float)individualSymbols.Length;
                for (int i = 0; i < length; i++) {
                    foreach (KeyValuePair<string, string> value in legend) {
                        if (value.Key == individualSymbols[i].ToString()) {
                            Vec2F pos = new Vec2F((0.5f - (length / 2f * 0.08f) + (i * 0.08f)), 
                            (0.95f - 0.025f * currentHeight));
                            CreateBlock((new DynamicShape(pos, new Vec2F(0.08f, 0.025f))), value.Value);
                        }
                    }
                }
            }
        }

        public void CreateBlock(DynamicShape shape, string color) {
            switch(color) {
                case "grey-block.png":
                    Blocks.AddEntity(new GreyBlock(shape));
                    break;
                case "brown-block.png":
                    Blocks.AddEntity(new BrownBlock(shape));
                    break;
                case "red-block.png":
                    Blocks.AddEntity(new RedBlock(shape));
                    break;
                case "orange-block.png":
                    Blocks.AddEntity(new OrangeBlock(shape));
                    break;
                case "yellow-block.png":
                    Blocks.AddEntity(new YellowBlock(shape));
                    break;
                case "green-block.png":
                    Blocks.AddEntity(new GreenBlock(shape));
                    break;
                case "darkgreen-block.png":
                    Blocks.AddEntity(new DarkgreenBlock(shape));
                    break;
                case "blue-block.png":
                    Blocks.AddEntity(new BlueBlock(shape));
                    break;
                case "teal-block.png":
                    Blocks.AddEntity(new TealBlock(shape));
                    break;
                case "purple-block.png":
                    Blocks.AddEntity(new PurpleBlock(shape));
                    break;
                default:
                    break;
            }
        }
    }
}