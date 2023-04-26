using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Blocks {
    public class BrownBlock : Block {
        public BrownBlock(DynamicShape shape) 
            : base(shape, new Image(Path.Combine("Assets", "Images", "brown-block.png")), 
            new Image(Path.Combine("Assets", "Images", "brown-block-damaged.png"))) {
                health = 8;
                fullHealth = health;
                value = 200;
            }
    }
}