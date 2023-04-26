using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Blocks {
    public class OrangeBlock : Block {
        public OrangeBlock(DynamicShape shape) 
            : base(shape, new Image(Path.Combine("Assets", "Images", "orange-block.png")), 
            new Image(Path.Combine("Assets", "Images", "orange-block-damaged.png"))) {
                health = 6;
                startHealth = health;
                value = 20;
            }
    }
}