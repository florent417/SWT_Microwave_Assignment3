using System;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System.Threading;


namespace Microwave.Test.Integration
{
    [TestFixture]
    public class Class1
    {
        private ITimer tim;
        private IDisplay dis;
        private IPowerTube pow;
        private ICookController uut;
        private IOutput output;
        private IUserInterface ui;

        [SetUp]
        public void SetUp()
        {
            output = Substitute.For<IOutput>();
            tim = new MicrowaveOvenClasses.Boundary.Timer();
            dis = new Display(output);
            pow = new PowerTube(output);
            ui = Substitute.For<IUserInterface>();
            uut = new CookController(tim, dis, pow, ui);
        }

        [Test]
        public void startcookoutput()
        {
            uut.StartCooking(95, 5);
            output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("powertube works with 95 %")));
        }

        [Test]
        public void cookwhilecooking()
        {
            uut.StartCooking(95, 5);
            Assert.That(() => uut.StartCooking(95, 5), Throws.TypeOf<ApplicationException>());
        }

        [TestCase(1100)]
        [TestCase(0)]
        public void poweroutofbounds(int power)
        {
            Assert.That(() => uut.StartCooking(power, 9), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void powertubeturnoff()
        {
            uut.StartCooking(95, 6);
            uut.Stop();
            output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("powertube turned off")));
        }

        [Test]
        public void turnoffaftertime()
        {
            uut.StartCooking(95, 1);
            Thread.Sleep(1500);
            output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("powertube turned off")));
        }

        [Test]
        public void displaytimeontimertick()
        {
            uut.StartCooking(95, 84);
            Thread.Sleep(3500);
            output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("display shows: 01:21")));
        }

        [Test]
        public void tolatenotdisplayed()
        {
            uut.StartCooking(95, 84);
            Thread.Sleep(3500);
            output.DidNotReceive().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("display shows: 01:20")));
        }

        [Test]
        public void toearlynotdisplayed()
        {
            uut.StartCooking(95, 84);
            Thread.Sleep(3500);
            output.DidNotReceive().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("display shows: 01:25")));
        }

        [Test]
        public void nodisplayticksiftimerstopped()
        {
            uut.StartCooking(95, 84);
            uut.Stop();
            Thread.Sleep(10000);
            output.DidNotReceive().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("display shows:")));
        }
    }
}