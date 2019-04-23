using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RouletteGame.Fields;
using RouletteGame.Randomizing;
using RouletteGame.Bets;

namespace RouletteGame.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            RouletteGame.Output.ConsoleOutput output = new RouletteGame.Output.ConsoleOutput();

            RouletteGame.Fields.IFieldFactory fieldFactory = new StandardFieldFactory();
            RouletteGame.Randomizing.IRandomizer randomizer = new RouletteRandomizer();

            RouletteGame.Roulette.IRoulette roulette = new RouletteGame.Roulette.Roulette(fieldFactory, randomizer);

            RouletteGame.Game.Game game = new RouletteGame.Game.Game(roulette, output);

            game.OpenBets();
            game.PlaceBet(new ColorBet("Player 1", 100, FieldColor.Black));
            game.PlaceBet(new ColorBet("Player 1", 100, FieldColor.Red));

            game.PlaceBet(new EvenOddBet("Player 2", 100, Parity.Even));
            game.PlaceBet(new EvenOddBet("Player 2", 100, Parity.Odd));

            for (uint i = 0; i < 36; i++)
                game.PlaceBet(new FieldBet("Player 3", 100, i));

            game.CloseBets();
            game.SpinRoulette();
            game.PayUp();
        }
    }
}
