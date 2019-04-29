using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace ClassLibrary2
{
    [TestFixture]
    class _4th_Itegration_TimerButton
    {
        private IButton _powerButton;
        private IButton _uut;
        private IButton _startCancelButton;
        private IDoor _door;
        private IUserInterface _userInterface;
        private ICookController _cookController;
        private ILight _light;
        private ITimer _timer;
        private IDisplay _display;
        private IPowerTube _powerTube;
        private IOutput _output;

        [SetUp]
        public void SetUp()
        {
            _powerButton = new Button();
            _uut = new Button();
            _startCancelButton = Substitute.For<IButton>();
            _door = Substitute.For<IDoor>();

            _output = Substitute.For<IOutput>();
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _timer = new Timer();
            _light = new Light(_output);
            _cookController = new CookController(_timer, _display, _powerTube, _userInterface);
            _userInterface = new UserInterface(_powerButton, _uut, _startCancelButton,
            _door, _display, _light, _cookController);
        }

        [Test]
        public void SetPower_TimeButton_OutputsTime1()
        {
            // Also checks if TimeButton is subscribed
            //_powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            //// Now in SetPower
            //_timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerButton.Press();
            _uut.Press();

            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("01:00")));
        }

        [Test]
        public void SetPower_2TimeButton_OutputsTime2()
        {
            //_powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            //// Now in SetPower
            //_timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            //_timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _powerButton.Press();
            _uut.Press();
            _uut.Press();

            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("02:00")));
        }

        [Test]
        public void SetPower_2TimeButton_OutputIsCorrect()
        {
            //_powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            //// Now in SetPower
            //_timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            //_timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _powerButton.Press();
            _uut.Press();
            _uut.Press();

            _output.DidNotReceive().OutputLine(Arg.Is<string>(str => str.Contains("03:00")));
        }
    }
}
