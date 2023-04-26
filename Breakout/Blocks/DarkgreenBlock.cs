using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Blocks {
    public class DarkgreenBlock : Block {
        public DarkgreenBlock(DynamicShape shape) 
            : base(shape, new Image(Path.Combine("Assets", "Images", "darkgreen-block.png")), 
            new Image(Path.Combine("Assets", "Images", "darkgreen-block-damaged.png"))) {
                health = 10;
                fullHealth = health;
                value = 300;
            }
    }
}