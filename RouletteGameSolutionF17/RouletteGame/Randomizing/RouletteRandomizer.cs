using System;

namespace RouletteGame.Randomizing
{
    public class RouletteRandomizer : IRandomizer
    {
        private readonly int _max;
        private readonly int _min;
        private readonly Random _random;

        public RouletteRandomizer()
        {
            _random = new Random();
            _min = 0;
            _max = 37;
        }
        
        // Excluded from coverage - manual test required
        public uint Next()
        {
            return (uint) _random.Next(_min, _max);
        }
    }
}