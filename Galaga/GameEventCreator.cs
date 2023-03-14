using DIKUArcade.Events;

namespace Galaga;

static public class GameEventCreator {
    static public GameEvent CreateGameEvent(GameEventType gameEventType, string message) {
        GameEvent retVal = new GameEvent();
        retVal.EventType = gameEventType;
        retVal.Message = message;
        return retVal;
    }
}