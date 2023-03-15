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
        return CreateGameEvent(GameEventType.MovementEvent, message);
    }
    static public GameEvent CreateWindowEvent(string message) {
        return CreateGameEvent(GameEventType.WindowEvent, message);
    }
    static public GameEvent CreateGameStateEvent(string message) {
        return CreateGameEvent(GameEventType.GameStateEvent, message);
    }

    static public GameEvent CreateStatusEvent(string message) {
        return CreateGameEvent(GameEventType.StatusEvent, message);
    }
}