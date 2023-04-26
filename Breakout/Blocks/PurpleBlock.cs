using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Blocks {
    public class PurpleBlock : Block {
        public PurpleBlock(DynamicShape shape) 
            : base(shape, new Image(Path.Combine("Assets", "Images", "purple-block.png")), 
            new Image(Path.Combine("Assets", "Images", "purple-block-damaged.png"))) {
                health = 6;
                startHealth = health;
                value = 50;
            }
    }
}