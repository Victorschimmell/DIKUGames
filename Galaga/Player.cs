using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga {
    public class Player {
        private Entity entity;
        private DynamicShape shape;
        private float moveLeft;
        private float moveRight;
        const float MOVEMENT_SPEED = 0.01f;
        public Player(DynamicShape shape, IBaseImage image) {
            entity = new Entity(shape, image);
            this.shape = shape;
            moveLeft = 0.0f;
            moveRight = 0.0f;
        }
        public void Render() {
            entity.RenderEntity();
        }
        public void Move() {
            // TODO: move the shape and guard against the window borders¨
            if (shape.Direction.X + shape.Position.X >= 0f && shape.Direction.X + shape.Position.X <= 1f-shape.Extent.X) {
                entity.Shape.Move(shape.Direction);
            }

        }
        public void SetMoveLeft(bool val) {
            if (val) {
                moveLeft = -MOVEMENT_SPEED;
            }
            else { moveLeft = 0; }
            UpdateDirection();
        }
        public void SetMoveRight(bool val) {
            if (val) {
                moveRight = MOVEMENT_SPEED;
            }
            else { moveRight = 0; }
            UpdateDirection();
            }
        
        private void UpdateDirection() {
            shape.Direction.X = moveLeft + moveRight;
        }
        public Vec2F GetPosition() {
            return this.shape.Position;
        }

        public Vec2F GetExtent() {
            return shape.Extent;
        }
    }
}   