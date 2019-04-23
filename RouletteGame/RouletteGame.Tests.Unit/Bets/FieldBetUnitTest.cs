using NSubstitute;
using NUnit.Framework;
using RouletteGame.Bets;
using RouletteGame.Fields;

namespace RouletteGame.Tests.Unit.Bets
{
    [TestFixture]
    public class FieldBetUnitTest
    {
        private FieldBet _uut;
        private IField _stubField;

        [SetUp]
        public void SetUp()
        {
            _uut = new FieldBet("Pete Mitchell", 100, 0);
            _stubField = Substitute.For<IField>();
        }
        [Test]
        public void FieldBet_ToString_ContainsCorrectValues()
        {
            Assert.That(_uut.ToString(), Is.StringMatching("100.*0"));
        }

        [TestCase(0, 3600)]
        [TestCase(1, 0)]
        public void FieldBet_DifferentFields_WonAmountCorrect(int fieldNumber, int wonAmount)
        {
            _stubField.Number.Returns((uint) fieldNumber);
            Assert.That(_uut.WonAmount(_stubField), Is.EqualTo(wonAmount));
        }
    }
}