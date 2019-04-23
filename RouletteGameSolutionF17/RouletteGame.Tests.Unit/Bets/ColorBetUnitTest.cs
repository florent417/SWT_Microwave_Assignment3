using NSubstitute;
using NUnit.Framework;
using RouletteGame.Bets;
using RouletteGame.Fields;

namespace RouletteGame.Tests.Unit.Bets
{
    [TestFixture]
    public class ColorBetUnitTest
    {
        private IField _stubField;
        private ColorBet _uutBlack;
        private ColorBet _uutRed;
        private ColorBet _uutGreen;

        [SetUp]
        public void SetUp()
        {
            _stubField = Substitute.For<IField>();
            _uutBlack = new ColorBet("Pete Mitchell", 100, FieldColor.Black);
            _uutRed = new ColorBet("Pete Mitchell", 100, FieldColor.Red);
            _uutGreen = new ColorBet("Pete Mitchell", 100, FieldColor.Green);
        }

        [Test]
        public void ColorBet_ToString_BlackBetContainsCorrectValues()
        {
            Assert.That(_uutBlack.ToString().ToLower(), Is.StringMatching("100.*black"));
        }


        [Test]
        public void ColorBet_ToString_GreenBetContainsCorrectValues()
        {
            Assert.That(_uutGreen.ToString().ToLower(), Is.StringMatching("100.*green"));
        }

        [Test]
        public void ColorBet_ToString_RedBetContainsCorrectValues()
        {
            Assert.That(_uutRed.ToString().ToLower(), Is.StringMatching("100.*red"));
        }

        [TestCase(FieldColor.Red, 0)]
        [TestCase(FieldColor.Black, 200)]
        [TestCase(FieldColor.Green, 0)]
        public void BlackBet_DifferentFieldColors_WonAmountCorrect(FieldColor color, int wonAmount)
        {
            _stubField.Color.Returns(color);
            Assert.That(_uutBlack.WonAmount(_stubField), Is.EqualTo(wonAmount));
        }


        [TestCase(FieldColor.Red, 200)]
        [TestCase(FieldColor.Black, 0)]
        [TestCase(FieldColor.Green, 0)]
        public void RedBet_DifferentFieldColors_WonAmountCorrect(FieldColor color, int wonAmount)
        {
            _stubField.Color.Returns(color);
            Assert.That(_uutRed.WonAmount(_stubField), Is.EqualTo(wonAmount));
        }

        [TestCase(FieldColor.Red, 0)]
        [TestCase(FieldColor.Black, 0)]
        [TestCase(FieldColor.Green, 200)]
        public void GreenBet_DifferentFieldColors_WonAmountCorrect(FieldColor color, int wonAmount)
        {
            _stubField.Color.Returns(color);
            Assert.That(_uutGreen.WonAmount(_stubField), Is.EqualTo(wonAmount));
        }
    }
}