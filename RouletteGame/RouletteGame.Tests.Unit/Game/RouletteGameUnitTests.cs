using System;
using System.Collections.Generic;
using NSubstitute;
using NSubstitute.Extensions;
using NUnit.Framework;
using RouletteGame.Bets;
using RouletteGame.Fields;
using RouletteGame.Game;
using RouletteGame.Output;
using RouletteGame.Roulette;

namespace RouletteGame.Tests.Unit.Game
{
    [TestFixture]
    public class RouletteGameUnitTest
    {
        private RouletteGame.Game.Game _uut;
        private IRoulette _fakeRoulette;
        private IOutput _fakeOutput;

        [SetUp]
        public void Setup()
        {
            // Make fresh copies for each test
            _fakeRoulette = Substitute.For<IRoulette>();
            _fakeOutput = Substitute.For<IOutput>();
            _uut = new RouletteGame.Game.Game(_fakeRoulette, _fakeOutput);
        }

        [Test]
        public void OpenBets_BetsOpened_BetsAreAnnouncedAsOpen()
        {
            _uut.OpenBets();
            _fakeOutput.Received().Report(Arg.Is<string>(str =>
                str.ToLower().Contains("open")
                ));
        }

        [Test]
        public void PlaceBet_CtorRoundNotOpen_NotAllowed()
        {
            Assert.Throws<RouletteGameException>(() => _uut.PlaceBet(Substitute.For<IBet>()));
        }


        [Test]
        public void PlaceBet_RoundIsOpen_Allowed()
        {
            _uut.OpenBets();
            Assert.DoesNotThrow(() => _uut.PlaceBet(Substitute.For<IBet>()));
        }

        [Test]
        public void PlaceBet_OpenCloseBets_NotAllowed()
        {
            _uut.OpenBets();
            _uut.CloseBets();
            Assert.Throws<RouletteGameException>(() => _uut.PlaceBet(Substitute.For<IBet>()));
        }

        [Test]
        public void CloseBets_BetsClose_BetsAreAnnouncedAsClosed()
        {
            _uut.OpenBets();
            _uut.CloseBets();
            _fakeOutput.Received().Report(Arg.Is<string>(str =>
                str.ToLower().Contains("closed")
                ));
        }

        [Test]
        public void SpinRoulette_CtorRoundNotOpened_SpinAllowed()
        {
            // Nothing has happened
            // TBD - what should happen on a spin?
            Assert.That(() => _uut.SpinRoulette(), Throws.Nothing);
        }

        [Test]
        public void SpinRoulette_RouletteIsSpun_RouletteIsReportedAsSpinning()
        {
            _uut.OpenBets();
            _uut.CloseBets();
            _uut.SpinRoulette();
            _fakeOutput.Received().Report(Arg.Is<string>(str =>
                str.ToLower().Contains("spinning")
                ));
        }

        [Test]
        public void SpinRoulette_BetsClosed_RouletteIsSpun()
        {
            _uut.OpenBets();
            _uut.CloseBets();
            _uut.SpinRoulette();

            _fakeRoulette.Received().Spin();
        }


        [Test]
        public void SpinRoulette_RoundOpen_SpinNotAllowed()
        {
            _uut.OpenBets();

            Assert.That(() => _uut.SpinRoulette(), Throws.TypeOf<RouletteGameException>());
        }

        [Test]
        public void SpinRoulette_RouletteSpun_ResultFieldAnnounced()
        {
            _uut.OpenBets();
            _uut.CloseBets();
            _uut.SpinRoulette();
            _fakeOutput.Received(1).Report(Arg.Is<string>(str =>
                str.ToLower().Contains("result: ")
                ));
        }

        [Test]
        public void PayUp_1BetPlaced_BetQueriedForWonAmount()
        {
            var bet = Substitute.For<IBet>();

            _uut.OpenBets();
            _uut.PlaceBet(bet);
            _uut.CloseBets();
            _uut.SpinRoulette();

            _uut.PayUp();
            bet.Received().WonAmount(Arg.Any<IField>());
        }

        [Test]
        public void PayUp_10BetsPlaced_AllBetsQueriedForWonAmount()
        {
            var bets = new List<IBet>();
            for (uint i = 0; i < 10; i++)
                bets.Add(Substitute.For<IBet>());

            _uut.OpenBets();
            foreach (var bet in bets)
                _uut.PlaceBet(bet);

            _uut.CloseBets();
            _uut.SpinRoulette();
            _uut.PayUp();

            foreach (var bet in bets)
            {
                bet.Received().WonAmount(Arg.Any<IField>());
            }
        }


        [Test]
        public void PayUp_RoundPlayed_RouletteQueriedForResult()
        {
            _uut.OpenBets();
            _uut.CloseBets();
            _uut.SpinRoulette();

            _uut.PayUp();

            _fakeRoulette.Received().GetResult();
        }

        [Test]
        public void PayUp_WinningBetPlaced_ReportContainsPlayerName()
        {
            var bet = Substitute.For<IBet>();

            bet.PlayerName.Returns("Pete Mitchell");
            bet.WonAmount(Arg.Any<IField>()).Returns(100U);

            _uut.OpenBets();
            _uut.PlaceBet(bet);
            _uut.CloseBets();
            _uut.SpinRoulette();

            _uut.PayUp();

            _fakeOutput.Received().Report(Arg.Is<string>(str => str.Contains("Pete Mitchell")));
        }

        [Test]
        public void Payup_InSecondRoundOfPlay_OldBetsNoMoreValid()
        { 
            var oldBet = Substitute.For<IBet>();

            _uut.OpenBets();
            _uut.PlaceBet(oldBet);
            _uut.CloseBets();
            _uut.SpinRoulette();

            _uut.PayUp();

            // Simulate a new round
            var newBet = Substitute.For<IBet>();

            _uut.OpenBets();
            _uut.PlaceBet(newBet);
            _uut.CloseBets();
            _uut.SpinRoulette();

            _uut.PayUp();

            // Old bet should only be used once
            oldBet.Received(1).WonAmount(Arg.Any<IField>());
            // And so should new bet
            newBet.Received(1).WonAmount(Arg.Any<IField>());


        }



    }
}