using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Blocks {
    public class BlueBlock : Block {
        public BlueBlock(DynamicShape shape) 
            : base(shape, new Image(Path.Combine("Assets", "Images", "blue-block.png")), 
            new Image(Path.Combine("Assets", "Images", "blue-block-damaged.png"))) {
                health = 4;
                fullHealth = health;
                value = 10;
            }
    }
}