using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using RouletteGame.Fields;
using RouletteGame.Game;
using RouletteGame.Randomizing;

namespace RouletteGame.Tests.Unit.Roulette
{
    [TestFixture]
    public class RouletteUnitTest
    {
        private List<IField> fakeList;
        private IRandomizer fakeRandomizer;
        private IFieldFactory fakeFieldFactory;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            fakeList = new List<IField>
            {
                Substitute.For<IField>(),
                Substitute.For<IField>(),
                Substitute.For<IField>()
            };

            fakeList[0].Number.Returns(0U);
            fakeList[1].Number.Returns(1U);
            fakeList[2].Number.Returns(6U);

            fakeList[0].Color.Returns(FieldColor.Green);
            fakeList[1].Color.Returns(FieldColor.Red);
            fakeList[2].Color.Returns(FieldColor.Black);

            fakeList[0].Parity.Returns(Parity.Neither);
            fakeList[1].Parity.Returns(Parity.Odd);
            fakeList[2].Parity.Returns(Parity.Even);

            fakeFieldFactory = Substitute.For<IFieldFactory>();
            fakeFieldFactory.CreateFields().Returns(fakeList);

        }

        [SetUp]
        public void Setup()
        {
            fakeRandomizer = Substitute.For<IRandomizer>();
        }

        [Test]
        public void Ctor_CallsCreateFields()
        {
            var uut = new RouletteGame.Roulette.Roulette(fakeFieldFactory, fakeRandomizer);
            fakeFieldFactory.Received(1).CreateFields();
        }

        [Test]
        public void GetResult_CtorRouletteNotSpun_NotAllowed()
        {
            var uut = new RouletteGame.Roulette.Roulette(fakeFieldFactory, fakeRandomizer);
            Assert.That(() => uut.GetResult(), Throws.TypeOf<RouletteGameException>());
        }

        [Test]
        public void GetResult_RouletteSpun_Allowed()
        {
            var uut = new RouletteGame.Roulette.Roulette(fakeFieldFactory, fakeRandomizer);
            uut.Spin();
            Assert.DoesNotThrow(() => uut.GetResult());
        }

        [Test]
        public void Spin_Call_NextCalled()
        {
            var uut = new RouletteGame.Roulette.Roulette(fakeFieldFactory, fakeRandomizer);
            uut.Spin();
            fakeRandomizer.Received(1).Next();
        }


        [Test]
        public void Spin_RandomizerReturnsIllegalValue_ExceptionThrown()
        {
            fakeRandomizer.Next().Returns(40U); // Always return '40' from randomizer
            var uut = new RouletteGame.Roulette.Roulette(fakeFieldFactory, fakeRandomizer);
            Assert.That(() => uut.Spin(), Throws.TypeOf<RouletteGameException>());
        }


        [Test]
        public void GetResult_Spin_ResultColorOK()
        {
            var uut = new RouletteGame.Roulette.Roulette(fakeFieldFactory, fakeRandomizer);
            uut.Spin();

            Assert.That(uut.GetResult().Color, Is.EqualTo(FieldColor.Green));
        }

        [Test]
        public void GetResult_Spin_ResultNumberOK()
        {
            var uut = new RouletteGame.Roulette.Roulette(fakeFieldFactory, fakeRandomizer);
            uut.Spin();
            Assert.That(uut.GetResult().Number, Is.EqualTo(0));
        }

        [Test]
        public void GetResult_SpinAndGetResult_ResultColorOK()
        {
            fakeRandomizer.Next().Returns(2U);

            var uut = new RouletteGame.Roulette.Roulette(fakeFieldFactory, fakeRandomizer);
            uut.Spin();

            Assert.That(uut.GetResult().Color, Is.EqualTo(FieldColor.Black));
        }

        [Test]
        public void GetResult_SpinAndGetResult_ResultNumberOK()
        {
            fakeRandomizer.Next().Returns(2U);

            var uut = new RouletteGame.Roulette.Roulette(fakeFieldFactory, fakeRandomizer);
            uut.Spin();

            Assert.That(uut.GetResult().Number, Is.EqualTo(6));
        }

        [Test]
        public void GetResult_SpinAndGetResultTwice_ResultsEqual()
        {
            fakeRandomizer.Next().Returns(2U);

            var uut = new RouletteGame.Roulette.Roulette(fakeFieldFactory, fakeRandomizer);
            uut.Spin();

            // First call
            var firstResult = uut.GetResult();

            Assert.That(uut.GetResult(), Is.EqualTo(firstResult));
        }

    }
}