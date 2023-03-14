using DIKUArcade.Events;

namespace Galaga;

/// <summary>
/// This class creates different GameEvents with certain GameEventTypes and given messages.
/// </summary>
static public class GameEventCreator {
    static public GameEvent CreateGameEvent(GameEventType gameEventType, string message) {
        GameEvent retVal = new GameEvent();
        retVal.EventType = gameEventType;
        retVal.Message = message;
        return retVal;
    }
    static public GameEvent CreateMovementEvent(string message) {
        GameEvent retVal = new GameEvent();
        retVal.EventType = GameEventType.MovementEvent;
        retVal.Message = message;
        return retVal;
    }
    static public GameEvent CreateWindowEvent(string message) {
        GameEvent retVal = new GameEvent();
        retVal.EventType = GameEventType.WindowEvent;
        retVal.Message = message;
        return retVal;
    }
    static public GameEvent CreateGameStateEvent(string message) {
        GameEvent retVal = new GameEvent();
        retVal.EventType = GameEventType.GameStateEvent;
        retVal.Message = message;
        return retVal;
    }
}