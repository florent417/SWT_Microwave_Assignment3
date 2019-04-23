using NUnit.Framework;
using RouletteGame.Fields;
using RouletteGame.Randomizing;

namespace RouletteGame.Tests.Integration
{
    [TestFixture]
    class IT5_RouletteRandomizer
    {
        private IRandomizer _randomizer;
        private Roulette.Roulette _roulette;
        private IFieldFactory _fieldFactory;

        [SetUp]
        public void SetUp()
        {
            _randomizer = new RouletteRandomizer();
            _fieldFactory = new StandardFieldFactory();
            _roulette = new Roulette.Roulette(_fieldFactory, _randomizer);
        }

        [Test]
        public void Spin_DoesNotThrow()
        {
            Assert.That(() => _roulette.Spin(), Throws.Nothing);
        }


        [Test]
        public void Spin_FieldNumberIsWithinBounds()
        {
            _roulette.Spin();
            Assert.That(_roulette.GetResult().Number, Is.
                GreaterThanOrEqualTo(0).And.
                LessThanOrEqualTo(36));
        }

        [Test]
        public void Spin_FieldColorIsWithinBounds()
        {
            _roulette.Spin();
            Assert.That(_roulette.GetResult().Color, Is.
                EqualTo(FieldColor.Red).Or.
                EqualTo(FieldColor.Black).Or.
                EqualTo(FieldColor.Green));
        }

        [Test]
        public void Spin_FieldEvenOddIsWithinBounds()
        {
            _roulette.Spin();
            Assert.That(_roulette.GetResult().Parity, Is.
                EqualTo(Parity.Even).Or.
                EqualTo(Parity.Odd).Or.
                EqualTo(Parity.Neither));
        }
    }
}
