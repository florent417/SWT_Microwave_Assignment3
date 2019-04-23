using System;
using System.Collections.Generic;
using RouletteGame.Bets;
using RouletteGame.Output;
using RouletteGame.Roulette;

namespace RouletteGame.Game
{
    public class Game : IGame
    {
        private readonly IRoulette _roulette;
        private readonly List<IBet> _bets;
        private readonly IOutput _output;
        private bool _roundIsOpen;

        public Game(IRoulette roulette, IOutput output)
        {
            _bets = new List<IBet>();
            _roulette = roulette;
            _output = output;
        }

        public void OpenBets()
        {
            // Clear old bets
            _bets.Clear();
            _output.Report("Round is open for bets");
            _roundIsOpen = true;
        }

        public void CloseBets()
        {
            _output.Report("Round is closed for bets");
            _roundIsOpen = false;
        }

        public void PlaceBet(IBet bet)
        {
            if (_roundIsOpen) _bets.Add(bet);
            else throw new RouletteGameException("Bet placed while round closed");
        }

        public void SpinRoulette()
        {
            if (_roundIsOpen) throw new RouletteGameException("Spin roulette while round open for bets");
            _output.Report("Spinning...");
            _roulette.Spin();
            _output.Report($"Result: {_roulette.GetResult()}");
        }

        public void PayUp()
        {
            var result = _roulette.GetResult();

            foreach (var bet in _bets)
            {
                var won = bet.WonAmount(result);
                if (won > 0)
                    _output.Report($"{bet.PlayerName} just won {won}$ on a {bet}");
            }
        }
    }

    public class RouletteGameException : Exception
    {
        public RouletteGameException(string s) : base(s)
        {
        }
    }
}