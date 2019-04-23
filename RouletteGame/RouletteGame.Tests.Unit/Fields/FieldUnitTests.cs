using NUnit.Framework;
using RouletteGame.Fields;

namespace RouletteGame.Tests.Unit.Fields
{
    [TestFixture]
    public class FieldUnitTest
    {
        
        [TestCase(FieldColor.Black)]
        [TestCase(FieldColor.Red)]
        [TestCase(FieldColor.Green)]
        public void Ctor_DifferentColorsSet_ColorIsCorrect(FieldColor color)
        {
            var uut = new Field(10, color);
            Assert.That(uut.Color, Is.EqualTo(color));
        }


        [TestCase(0)]
        [TestCase(36)]
        public void Ctor_ValidFieldNumbers_NumberIsCorrect(int number)
        {
            var uut = new Field((uint) number, FieldColor.Black);
            Assert.That(uut.Number, Is.EqualTo(number));
        }

        [Test]
        public void Field_Create_Number37ExceptionIsThrown()
        {
            Assert.That(() =>  new Field(37, FieldColor.Black), Throws.TypeOf<FieldException>());
        }


        [TestCase(0, Parity.Neither)]
        [TestCase(1, Parity.Odd)]
        [TestCase(2, Parity.Even)]
        public void Field_Create_Value0ParityIsNeither(int number, Parity parity)
        {
            var uut = new Field((uint) number, FieldColor.Green);
            Assert.That(uut.Parity, Is.EqualTo(parity));
        }


        [Test]
        public void Field_ToString_BlackContainsCorrectValues()
        {
            var uut = new Field(3, FieldColor.Black);
            Assert.That(uut.ToString().ToLower(), Is.StringMatching("3.*black"));
        }

        [Test]
        public void Field_ToString_GreenContainsCorrectValues()
        {
            var uut = new Field(0, FieldColor.Green);
            Assert.That(uut.ToString().ToLower(), Is.StringMatching("0.*green"));
        }

        [Test]
        public void Field_ToString_RedContainsCorrectValues()
        {
            var uut = new Field(4, FieldColor.Red);
            Assert.That(uut.ToString().ToLower(), Is.StringMatching("4.*red"));
        }
    }
}