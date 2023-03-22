using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga.GameText {
public class RoundCounter: IGameText {
    private int round;
    private Text display;
    public Text Display => display;
    public RoundCounter (Vec2F position, Vec2F extent) {
        round = 0;
        display = new Text (round.ToString(), position, extent);
        display.SetColor(new Vec3F(1f, 1f, 1f));
    }
    public RoundCounter (Text display, int round) {
        this.round = round;
        this.display = new Text (round.ToString(), display.GetShape().Position, display.GetShape().Extent);
        this.display.SetColor(new Vec3F(1f, 1f, 1f));
    }
    public void IncrementRound() {
        round++;
    }
    public void RenderGameText () {
        display.SetText("Round: " + round.ToString());
        display.RenderText();
    }

    public RoundCounter Clone() {
        return new RoundCounter(Display, round);
    }
}
}