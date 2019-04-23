using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using RouletteGame.Bets;
using RouletteGame.Output;
using RouletteGame.Randomizing;
using RouletteGame.Fields;

namespace RouletteGame.Tests.Integration
{
    [TestFixture]
    class IT7_RouletteGameRandomizer
    {
        private Game.Game _game;
        private IOutput _output;

        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();

            var roulette = new Roulette.Roulette(new StandardFieldFactory(), new RouletteRandomizer());
            _game = new Game.Game(roulette, _output);
        }

        [Test]
        public void SpinRoulette_AllResultsPossible_ShowSomeResult()
        {
            _game.OpenBets();
            _game.CloseBets();
            _game.SpinRoulette();
            _output.Received().Report(Arg.Is<string>(str =>
                str.ToLower().Contains("result:") &&
                (str.ToLower().Contains("black") ||
                 str.ToLower().Contains("red") ||
                 str.ToLower().Contains("green"))
                ));

        }

        [Test]
        public void PayUp_AllEvenOddBets_ShowSomeWinner()
        {
            _game.OpenBets();

            _game.PlaceBet(new EvenOddBet("Bjarne", 100, Parity.Even));
            _game.PlaceBet(new EvenOddBet("Bjarne", 100, Parity.Odd));
            _game.PlaceBet(new EvenOddBet("Bjarne", 100, Parity.Neither));

            _game.CloseBets();
            _game.SpinRoulette();
            _game.PayUp();

            _output.Received(1).Report(Arg.Is<string>(str =>
                str.ToLower().Contains("bjarne") &&
                str.ToLower().Contains("100")
                ));
        }


    }
}
