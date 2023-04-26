using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Blocks {
    public class RedBlock : Block {
        public RedBlock(DynamicShape shape) 
            : base(shape, new Image(Path.Combine("Assets", "Images", "red-block.png")), 
            new Image(Path.Combine("Assets", "Images", "red-block-damaged.png"))) {
                health = 15;
                fullHealth = health;
                value = 750;
            }
    }
}