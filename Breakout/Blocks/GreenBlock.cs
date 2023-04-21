using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Blocks {
    public class GreenBlock : Block {
        public GreenBlock(DynamicShape shape) 
            : base(shape, new Image(Path.Combine("Assets", "Images", "green-block.png")), 
            new Image(Path.Combine("Assets", "Images", "green-block-damaged.png"))) {
                health = 12;
                startHealth = health;
                value = 200;
            }
    }
}