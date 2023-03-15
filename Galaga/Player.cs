using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Galaga {
    public class Player: Entity, IGameEventProcessor {
        private DynamicShape shape;
        private float moveLeft;
        private float moveRight;
        const float MOVEMENT_SPEED = 0.01f;
        public Player(DynamicShape shape, IBaseImage image): base(shape, image) {
            this.shape = shape;
            moveLeft = 0.0f;
            moveRight = 0.0f;
        }
        public void Render() {
            if (!IsDeleted()) {
                RenderEntity();
            }
        }
        public void Move() {
            if (shape.Direction.X + shape.Position.X >= 0f && shape.Direction.X + shape.Position.X <= 1f-shape.Extent.X) {
                Shape.Move(shape.Direction);
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
        
        private void UpdateDirection() {
            shape.Direction.X = moveLeft + moveRight;
        }
        public Vec2F GetPosition() {
            return this.shape.Position;
        }

        public Vec2F GetExtent() {
            return shape.Extent;
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
                    case "MoveStop":
                        SetMoveLeft(false);
                        SetMoveRight(false);
                        break;
                    case "MoveAll":
                        shape.Move();
                        break;
                    default:
                        break;
                }
            }
            else if (gameEvent.EventType == GameEventType.GameStateEvent) {
                switch (gameEvent.Message) {
                    case "GameOver": 
                        DeleteEntity();
                        break;
                }
            }
        }
}   
}