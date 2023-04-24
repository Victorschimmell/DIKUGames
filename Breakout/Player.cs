using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout{
    public class Player: Entity, IGameEventProcessor {
        private DynamicShape shape;
        private float moveLeft;
        private float moveRight;
        private int points;
        const float MOVEMENT_SPEED = 0.01f;
        public Player(DynamicShape shape, IBaseImage image): base(shape, image){
            this.shape = shape;
            moveLeft = 0.0f;
            moveRight = 0.0f;
            points = 0;
        }

        public void Render() {
            if (!IsDeleted()) {
                RenderEntity();
            }
        }

        public void Move() {
            if (shape.Direction.X + shape.Position.X >= 0f && shape.Direction.X + shape.Position.X <= 1f-shape.Extent.X) {
                Shape.MoveX(shape.Direction.X);
            } else if (shape.Direction.X + shape.Position.X < 0f) {
                BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                    EventType = GameEventType.MovementEvent,
                    Message = "MoveRight"
                });
            } else if (shape.Direction.X + shape.Position.X > 1f-shape.Extent.X) {
                BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                    EventType = GameEventType.MovementEvent,
                    Message = "MoveLeft"
                });
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
                    case "MoveStopAll":
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

        public void AddPoints(int p) {
            points = points + p;
        }
    }
}