using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Blocks {
    public class TealBlock : Block {
        public TealBlock(DynamicShape shape) 
            : base(shape, new Image(Path.Combine("Assets", "Images", "teal-block.png")), 
            new Image(Path.Combine("Assets", "Images", "teal-block-damaged.png"))) {
                health = 20;
                startHealth = health;
                value = 750;
            }
    }
}