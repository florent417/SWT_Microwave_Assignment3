using System;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;


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
            tim = new Timer();
            dis = new Display(output);
            pow = new PowerTube(output);
            ui = Substitute.For<IUserInterface>();
            uut = new CookController(tim, dis, pow, ui);
        }

        [Test]
        public void startcook_powertubeoutput()
        {
            uut.StartCooking(95, 5);
            output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("PowerTube works with 95 %")));
        }
    }
}
