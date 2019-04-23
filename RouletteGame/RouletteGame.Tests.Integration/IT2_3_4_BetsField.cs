using NUnit.Framework;
using RouletteGame.Bets;
using RouletteGame.Fields;

namespace RouletteGame.Tests.Integration
{
    [TestFixture]
    class IT2_3_4_BetsField
    {
        private IField _winField;
        private IField _loseField;
        private FieldBet _fieldBet;
        private ColorBet _colorBet;
        private EvenOddBet _evenOddBet;
        private const int BetAmount = 100;

        [SetUp]
        public void SetUp()
        {
            _winField = new Field(10, FieldColor.Red);          // 10, even, red
            _loseField = new Field(11, FieldColor.Black);       // 11, odd, black
            _fieldBet = new FieldBet("John Doe", BetAmount, 10);
            _colorBet = new ColorBet("John Doe", BetAmount, FieldColor.Red);
            _evenOddBet = new EvenOddBet("John Doe", BetAmount, Parity.Even);
        }

        #region FieldBet
        [Test]
        public void FieldBet_BetIsWon_WonAmountIsCorrect()
        {
            Assert.That(_fieldBet.WonAmount(_winField), Is.EqualTo(36* BetAmount));
        }

        [Test]
        public void FieldBet_BetIsLost_WonAmountIsCorrect()
        {
            Assert.That(_fieldBet.WonAmount(_loseField), Is.EqualTo(0));
        }

        #endregion

        #region ColorBet
        [Test]
        public void ColorBet_BetIsWon_WonAmountIsCorrect()
        {
            Assert.That(_colorBet.WonAmount(_winField), Is.EqualTo(2 * BetAmount));
        }

        [Test]
        public void ColorBet_BetIsLost_WonAmountIsCorrect()
        {
            Assert.That(_colorBet.WonAmount(_loseField), Is.EqualTo(0));
        }
        #endregion

        #region EvenOddBet
        [Test]
        public void EvenOddBet_BetIsWon_WonAmountIsCorrect()
        {
            Assert.That(_evenOddBet.WonAmount(_winField), Is.EqualTo(2 * BetAmount));
        }

        [Test]
        public void EvenOddBet_BetIsLost_WonAmountIsCorrect()
        {
            Assert.That(_evenOddBet.WonAmount(_loseField), Is.EqualTo(0));
        }
        #endregion
    }
}
