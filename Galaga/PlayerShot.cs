using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Galaga;
public class PlayerShot : Entity, IGameEventProcessor {
    private static Vec2F extent = new Vec2F(0.008f, 0.021f);
    private static Vec2F direction = new Vec2F(0.0f, 0.1f);
    public PlayerShot(Vec2F position, IBaseImage image)
        : base(new DynamicShape(position, extent, direction), image) {}
    
    public static Vec2F GetExtent() {
        return extent;
    }
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.MovementEvent) {
            switch (gameEvent.Message) {
                case "MoveAll":
                Shape.Move();
                break;
            }
        }
    }
}