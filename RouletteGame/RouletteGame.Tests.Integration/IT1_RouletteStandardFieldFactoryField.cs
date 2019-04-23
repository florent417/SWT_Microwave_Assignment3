using NSubstitute;
using NUnit.Framework;
using RouletteGame.Fields;
using RouletteGame.Game;
using RouletteGame.Randomizing;

namespace RouletteGame.Tests.Integration
{
    [TestFixture]
    public class IT1_RouletteStandardFieldFactoryField
    {
        private IFieldFactory _fieldFactory;
        private IRandomizer _randomizer;
        private Roulette.Roulette _uut;

        [SetUp]
        public void SetUp()
        {
            _fieldFactory = new StandardFieldFactory();
            _randomizer = Substitute.For<IRandomizer>();
            _uut = new Roulette.Roulette(_fieldFactory, _randomizer);
        }


        [TestCase(0)]
        [TestCase(36)]
        public void Spin_FieldNumberOK_Allowed(int number)
        {
            _randomizer.Next().Returns((uint)number);
            Assert.That(() => _uut.Spin(), Throws.Nothing);
        }


        [Test]
        public void Spin_FieldNumberOutOfRange_NotAllowed()
        {
            _randomizer.Next().Returns((uint)37);
            Assert.That(() => _uut.Spin(), Throws.TypeOf<RouletteGameException>());
        }

        [TestCase(0, FieldColor.Green)]
        [TestCase(1, FieldColor.Red)]
        [TestCase(2, FieldColor.Black)]
        public void GetResult_DifferentFieldsAsResult_FieldColorIsCorrect(int fieldNumber, FieldColor color)
        {
            _randomizer.Next().Returns((uint)fieldNumber);
            _uut.Spin();
            Assert.That(_uut.GetResult().Color, Is.EqualTo(color));
        }


    }
}
