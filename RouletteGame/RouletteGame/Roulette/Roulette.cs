using System.Collections.Generic;
using RouletteGame.Fields;
using RouletteGame.Game;
using RouletteGame.Randomizing;

namespace RouletteGame.Roulette
{
    public class Roulette : IRoulette
    {
        private readonly IRandomizer _randomizer;
        protected readonly List<IField> Fields;
        private IField _result;

        public Roulette(IFieldFactory fieldFactory, IRandomizer randomizer)
        {
            Fields = fieldFactory.CreateFields();
            _result = null;
            _randomizer = randomizer;
        }

        public void Spin()
        {
            var n = _randomizer.Next();
            if (Fields.Count > n) _result = Fields[(int) n];
            else throw new RouletteGameException($"Illegal field number: {n}");
        }

        public IField GetResult()
        {
            if (_result == null)
                throw new RouletteGameException("GetResult called before first spin");
            return _result;
        }
    }
}