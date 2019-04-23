using NUnit.Framework;
using RouletteGame.Randomizing;

namespace RouletteGame.Tests.Unit.Fields.Randomizing
{
    [TestFixture]
    public class RouletteRandomizerUnitTest
    {
        [Test]
        public void RouletteRandomizer_NextOK()
        {
            var uut = new RouletteRandomizer();
            var result = uut.Next();
            Assert.That(result <= 36); // Not exactly exhaustive
        }
    }
}