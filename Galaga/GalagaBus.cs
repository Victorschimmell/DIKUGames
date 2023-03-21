using DIKUArcade.Events;
using System.Collections.Generic;

namespace Galaga {
    public static class GalagaBus {
        private static GameEventBus eventBus;
        public static GameEventBus GetBus() {
            return GalagaBus.eventBus ?? InitializeGalagaEventBus();
        }
        private static GameEventBus InitializeGalagaEventBus() {
            GalagaBus.eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent,
            GameEventType.WindowEvent, GameEventType.MovementEvent, GameEventType.GameStateEvent});
            return GalagaBus.eventBus;
        }
    }
}

