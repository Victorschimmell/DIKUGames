using System.Collections.Generic;

namespace Galaga.GameText {
public class GameTextContainer {
    private List<IGameText> gameTexts;
    public GameTextContainer() {
        gameTexts = new List<IGameText>();
    }
    public void AddText(IGameText gameText) {
        gameTexts.Add(gameText);
    }
    public void RemoveText(IGameText gameText) {
        gameTexts.Remove(gameText);
    }
    public void RenderTexts() {
        foreach (IGameText gameText in gameTexts) {
            gameText.RenderGameText();
        }
    }
}
}