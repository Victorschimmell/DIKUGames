using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Galaga {
    public class Health {
    private int health;
    private Text display;
    public Health (Vec2F position, Vec2F extent) {
        health = 3;
        display = new Text (health.ToString(), position, extent);
        display.SetColor(new Vec3F(1f,1f,1f));
        display.SetFontSize(100);
    }
    // Remember to explaination your choice as to what happens
    //when losing health.
    public void LoseHealth (GameEventBus eventBus) {
        health -= 1;
        if (health <= 0) {
            GameEvent lose = new GameEvent();
            lose.EventType = GameEventType.GameStateEvent;
            lose.Message = "Game Over";
            eventBus.RegisterEvent(lose);
        }
    }
    public void RenderHealth () {
        display.SetText(health.ToString());
        display.RenderText();
    }
}
}