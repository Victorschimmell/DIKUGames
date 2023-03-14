using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga.GameText {
public class GameStateText: IGameText {
    private Text display;
    public Text Display => display;
    public GameStateText (string text, Vec2F position, Vec2F extent) {
        display = new Text(text, new Vec2F(0.2f, 0f), new Vec2F(0.8f, 0.8f));
        display.SetColor(new Vec3F(1f, 1f, 1f));
    }
    public void ChangeText(string text) {
        display.SetText(text);
    }
    public void RenderGameText () {
        display.RenderText();
    }
}
}