using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga.GameText {
    public class Health: IGameText {
    private int health;
    private Text display;
    public Text Display => display;
    public Health (Vec2F position, Vec2F extent) {
        health = 3;
        display = new Text (health.ToString(), position, extent);
        display.SetColor(new Vec3F(1f,1f,1f));
    }
    /// <summary>
    /// This method reduces the health by 1 and checks whether or not the HP is 0. The method returns 
    /// health after decreasing it. It is then handled in Game because it is not Health's responsibility
    /// to handle what it means, it is Game's responsibility. 
    /// </summary>
    public int LoseHealth () {
        health -= 1;
        return health;
    }
    public void RenderGameText () {
        display.SetText("HP: " + health.ToString());
        display.RenderText();
    }
}
}