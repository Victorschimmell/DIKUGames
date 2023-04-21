using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Blocks {
    public class YellowBlock : Block {
        public YellowBlock(DynamicShape shape) 
            : base(shape, new Image(Path.Combine("Assets", "Images", "yellow-block.png")), 
            new Image(Path.Combine("Assets", "Images", "yellow-block-damaged.png"))) {
                health = 10;
                startHealth = health;
                value = 150;
            }
    }
}