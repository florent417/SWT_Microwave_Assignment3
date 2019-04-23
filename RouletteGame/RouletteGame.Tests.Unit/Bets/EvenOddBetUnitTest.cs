using NSubstitute;
using NUnit.Framework;
using RouletteGame.Bets;
using RouletteGame.Fields;

namespace RouletteGame.Tests.Unit.Bets
{
    [TestFixture]
    public class ParityBetUnitTest
    {
        private EvenOddBet _uutEven;
        private EvenOddBet _uutOdd;
        private EvenOddBet _uutNeither;
        private IField _stubField;

        [SetUp]
        public void SetUp()
        {
            _uutEven = new EvenOddBet("Pete Mitchell", 100, Parity.Even);
            _uutOdd = new EvenOddBet("Pete Mitchell", 100, Parity.Odd);
            _uutNeither = new EvenOddBet("Pete Mitchell", 100, Parity.Neither);
            _stubField = Substitute.For<IField>();
        }

        [Test]
        public void EvenBet_ToString_EvenBetContainsCorrectValues()
        {
            Assert.That(_uutEven.ToString().ToLower(), Is.StringMatching("100.*even"));
        }

        [Test]
        public void OddBet_ToString_EvenBetContainsCorrectValues()
        {
            Assert.That(_uutOdd.ToString().ToLower(), Is.StringMatching("100.*odd"));
        }

        [Test]
        public void NeitherBet_ToString_NeitherBetContainsCorrectValues()
        {
            Assert.That(_uutNeither.ToString().ToLower(), Is.StringMatching("100.*neither"));
        }


        [TestCase(Parity.Even, 200)]
        [TestCase(Parity.Odd, 0)]
        [TestCase(Parity.Neither, 0)]
        public void EvenBet_DifferentFields_WonAmountCorrect(Parity parity, int wonAmount)
        {
            _stubField.Parity.Returns(parity);
            Assert.That(_uutEven.WonAmount(_stubField), Is.EqualTo(wonAmount));
        }


        [TestCase(Parity.Even, 0)]
        [TestCase(Parity.Odd, 200)]
        [TestCase(Parity.Neither, 0)]
        public void OddBet_DifferentFields_WonAmountCorrect(Parity parity, int wonAmount)
        {
            _stubField.Parity.Returns(parity);
            Assert.That(_uutOdd.WonAmount(_stubField), Is.EqualTo(wonAmount));
        }

        [TestCase(Parity.Even, 0)]
        [TestCase(Parity.Odd, 0)]
        [TestCase(Parity.Neither, 200)]
        public void NeitherBet_DifferentFields_WonAmountCorrect(Parity parity, int wonAmount)
        {
            _stubField.Parity.Returns(parity);
            Assert.That(_uutNeither.WonAmount(_stubField), Is.EqualTo(wonAmount));
        }



    }
}