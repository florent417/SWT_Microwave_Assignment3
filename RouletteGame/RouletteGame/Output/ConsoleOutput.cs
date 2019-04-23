using System;

namespace RouletteGame.Output
{
    public class ConsoleOutput : IOutput
    {
        // Excluded from coverage - manual test required
        public void Report(string arg)
        {
            Console.WriteLine("Roulette says: " + arg);
        }
    }
}