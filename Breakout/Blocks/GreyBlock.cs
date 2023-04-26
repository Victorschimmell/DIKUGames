using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Blocks {
    public class GreyBlock : Block {
        public GreyBlock(DynamicShape shape) 
            : base(shape, new Image(Path.Combine("Assets", "Images", "grey-block.png")), 
            new Image(Path.Combine("Assets", "Images", "grey-block-damaged.png"))) {
                health = 2;
                fullHealth = health;
                value = 5;
            }
    }
}