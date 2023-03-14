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
    /// <summary>
    /// This method reduces the health by 1 and checks whether or not the HP is 0. If the HP is 0 or 
    /// less then the method returns true indicating that the game is over. The method does not end 
    /// the game itself, because that is Game's responsibility. 
    /// </summary>
    public bool LoseHealth () {
        health -= 1;
        if (health <= 0) {
            return true;
        }
        return false;
    }
    public void RenderHealth () {
        display.SetText(health.ToString());
        display.RenderText();
    }
}
}