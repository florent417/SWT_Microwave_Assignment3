using NUnit.Framework;
using RouletteGame.Fields;

namespace RouletteGame.Tests.Unit.Fields
{
    [TestFixture]
    public class StandardFieldFactoryUnitTest
    {

        [Test]
        public void FieldFactory_CreateFields_CountOK()
        {
            var uut = new StandardFieldFactory();
            Assert.That(uut.CreateFields().Count, Is.EqualTo(37));
        }

        [TestCase(0, FieldColor.Green)]
        [TestCase(1, FieldColor.Red)]
        [TestCase(2, FieldColor.Black)]
        [TestCase(3, FieldColor.Red)]
        [TestCase(4, FieldColor.Black)]
        [TestCase(5, FieldColor.Red)]
        [TestCase(6, FieldColor.Black)]
        [TestCase(7, FieldColor.Red)]
        [TestCase(8, FieldColor.Black)]
        [TestCase(9, FieldColor.Red)]
        [TestCase(10, FieldColor.Black)]
        [TestCase(11, FieldColor.Black)]
        [TestCase(12, FieldColor.Red)]
        [TestCase(13, FieldColor.Black)]
        [TestCase(14, FieldColor.Red)]
        [TestCase(15, FieldColor.Black)]
        [TestCase(16, FieldColor.Red)]
        [TestCase(17, FieldColor.Black)]
        [TestCase(18, FieldColor.Red)]
        [TestCase(19, FieldColor.Red)]
        [TestCase(20, FieldColor.Black)]
        [TestCase(21, FieldColor.Red)]
        [TestCase(22, FieldColor.Black)]
        [TestCase(23, FieldColor.Red)]
        [TestCase(24, FieldColor.Black)]
        [TestCase(25, FieldColor.Red)]
        [TestCase(26, FieldColor.Black)]
        [TestCase(27, FieldColor.Red)]
        [TestCase(28, FieldColor.Black)]
        [TestCase(29, FieldColor.Black)]
        [TestCase(30, FieldColor.Red)]
        [TestCase(31, FieldColor.Black)]
        [TestCase(32, FieldColor.Red)]
        [TestCase(33, FieldColor.Black)]
        [TestCase(34, FieldColor.Red)]
        [TestCase(35, FieldColor.Black)]
        [TestCase(36, FieldColor.Red)]
        public void FieldFactory_CreateFields_FieldColorsAreCorrect(int fieldNumber, FieldColor color)
        {
            var list = new StandardFieldFactory().CreateFields();
            var index = list.FindIndex(field => field.Number == fieldNumber);
            Assert.That(list[index].Color, Is.EqualTo(color));
        }
    }
}