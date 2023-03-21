using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Galaga {
    public class Player: Entity, IGameEventProcessor {
        private DynamicShape shape;
        private float moveLeft;
        private float moveRight;
        private float moveUp;
        private float moveDown;
        const float MOVEMENT_SPEED = 0.01f;
        public Player(DynamicShape shape, IBaseImage image): base(shape, image) {
            this.shape = shape;
            moveLeft = 0.0f;
            moveRight = 0.0f;
            moveUp = 0.0f;
            moveDown = 0.0f;
        }
        public void Render() {
            if (!IsDeleted()) {
                RenderEntity();
            }
        }
        public void Move() {
            if (shape.Direction.X + shape.Position.X >= 0f && shape.Direction.X + shape.Position.X <= 1f-shape.Extent.X) {
                Shape.MoveX(shape.Direction.X);
            }
            if (shape.Direction.Y + shape.Position.Y >= 0f && shape.Direction.Y + shape.Position.Y <= 1f-shape.Extent.Y) {
                Shape.MoveY(shape.Direction.Y);
            }

        }
        private void SetMoveLeft(bool val) {
            if (val) {
                moveLeft = -MOVEMENT_SPEED;
            }
            else { moveLeft = 0; }
            UpdateDirection();
        }
        private void SetMoveRight(bool val) {
            if (val) {
                moveRight = MOVEMENT_SPEED;
            }
            else { moveRight = 0; }
            UpdateDirection();
        }
        private void SetMoveUp(bool val) {
            if (val) {
                moveUp = MOVEMENT_SPEED;
            }
            else { moveUp = 0; }
            UpdateDirection();
        }
        private void SetMoveDown(bool val) {
            if (val) {
                moveDown = -MOVEMENT_SPEED;
            }
            else { moveDown = 0; }
            UpdateDirection();
        }
        
        private void UpdateDirection() {
            shape.Direction.X = moveLeft + moveRight;
            shape.Direction.Y = moveUp + moveDown;
        }
        public Vec2F GetPosition() {
            return this.shape.Position;
        }

        public Vec2F GetExtent() {
            return shape.Extent;
        }
        public DynamicShape GetShape() {
            return shape;
        }
        void IGameEventProcessor.ProcessEvent(GameEvent gameEvent ) {
            if (gameEvent.EventType == GameEventType.MovementEvent) {
                switch (gameEvent.Message) {
                    case "MoveLeft": 
                        SetMoveLeft(true);
                        SetMoveRight(false);
                        break;
                    case "MoveRight":
                        SetMoveLeft(false);
                        SetMoveRight(true);
                        break;
                    case "MoveUp":
                        SetMoveUp(true);
                        SetMoveDown(false);
                        break;
                    case "MoveDown":
                        SetMoveUp(false);
                        SetMoveDown(true);
                        break;
                    case "MoveStopLeftRight":
                        SetMoveLeft(false);
                        SetMoveRight(false);
                        break;
                    case "MoveStopUpDown":
                        SetMoveUp(false);
                        SetMoveDown(false);
                        break;
                    case "MoveStopAll":
                        SetMoveUp(false);
                        SetMoveDown(false);
                        SetMoveLeft(false);
                        SetMoveRight(false);
                        break;
                    case "MoveAll":
                        Move();
                        break;
                    default:
                        break;
                }
            }
        }
}   
}