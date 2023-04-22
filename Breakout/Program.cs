using System;
using DIKUArcade.GUI;

namespace Breakout {
    class Program {
        static void Main(string[] args)
        {
            var windowArgs = new WindowArgs() { Title = "Atari Breakout - KU Version 2023" };
            var game = new Game(windowArgs);
            game.Run();
        }
    }
}
