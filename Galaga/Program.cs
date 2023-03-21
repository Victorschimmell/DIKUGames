using System;
using DIKUArcade.GUI;

namespace Galaga
{
    class Program
    {
        static void Main(string[] args)
        {
            var windowArgs = new WindowArgs() { Title = "Galaga v0.1" };
            var game = new GameV2(windowArgs);
            game.Run();
        }
    }
}
