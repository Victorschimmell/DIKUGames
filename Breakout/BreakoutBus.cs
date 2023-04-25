using DIKUArcade.Events;
using System.Collections.Generic;

namespace Breakout {
    public static class BreakoutBus {
        private static GameEventBus eventBus;
        public static GameEventBus GetBus() {
            return BreakoutBus.eventBus ?? InitializeBreakoutEventBus();
        }
        private static GameEventBus InitializeBreakoutEventBus() {
            BreakoutBus.eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent,
            GameEventType.WindowEvent, GameEventType.MovementEvent, GameEventType.GameStateEvent});
            return BreakoutBus.eventBus;
        }
    }
}