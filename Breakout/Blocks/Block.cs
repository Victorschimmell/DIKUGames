using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Blocks {
    public class Block : Entity {
        internal int startHealth;
        internal int health;
        internal int value;
        public int Value {get {return value;}}
        private bool isDamaged;
        private Vec2F position;
        private IBaseImage alternativeImage;
        public Block(DynamicShape shape, IBaseImage image, IBaseImage alternativeImage) 
            : base(shape, image) {
                health = 2;
                startHealth = health;
                value = 5;
                position = new Vec2F(shape.Position.X, shape.Position.Y);
                this.alternativeImage = alternativeImage;
                isDamaged = false;
            }

        public void Render() {
            if (!IsDeleted()) {
                RenderEntity();
            }
        }

        public void TakeDamage() {
            health--;
            if (health <= 0) {
                DeleteEntity();
            }
            if (!isDamaged && health <= startHealth / 2) {
                Image = alternativeImage;
                isDamaged = true;
            }
        }
    }
}