using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga.GameText {
    public class Health: IGameText {
    private int health;
    private int maxHealth;
    private Text display;
    public Text Display => display;
    public Health (Vec2F position, Vec2F extent) {
        maxHealth = 3;
        health = maxHealth;
        display = new Text (health.ToString(), position, extent);
        display.SetColor(new Vec3F(1f, 1f, 1f));
    }
    /// <summary>
    /// This method reduces the health by an amount and checks whether or not the HP is 0. The method  
    /// returns health after decreasing it. It is then handled in Game because it is not Health's 
    /// responsibility to handle what it means, it is Game's responsibility. 
    /// The hp is not reduced if the amount to lose is negative and the hp can't be reduced to less
    /// than 0.
    /// If the player collides with the enemy, the player will lose all its hp and lose the game.
    /// When an enemy collides with the bottom of the screen, the player will lose 1 hp. But this
    /// part is not handled here. It is handled in GameRunning.
    /// </summary>
    public int LoseHealth (int amount) {
        if (amount > 0) {
            health = System.Math.Max(0, health - amount);
        }
        return health;
    }
    public void RenderGameText () {
        display.SetText("HP: " + health.ToString());
        display.RenderText();
    }
}
}