using NUnit.Framework;
using RouletteGame.Bets;

namespace RouletteGame.Tests.Unit.Bets
{
    [TestFixture]
    public class BetUnitTest
    {
        private FieldBet _uut;  // Use a derivative of UUT type to test UUT functionality

        [SetUp]
        public void SetUp()
        {
            _uut = new FieldBet("Pete Mitchell", 100, 0);
        }
        [Test]
        public void Bet_Create_AmountIsOK()
        {
            Assert.That(_uut.Amount, Is.EqualTo(100));
        }

        [Test]
        public void Bet_Create_NameIsOK()
        {
            Assert.That(_uut.PlayerName, Is.EqualTo("Pete Mitchell"));
        }
    }
}