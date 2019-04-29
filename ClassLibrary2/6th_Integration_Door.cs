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
    class _6th_Integration_Door
    {
        private IButton _powerButton;
        private IButton _timerButton;
        private IButton _startCancelButton;
        private IDoor _uut;
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
            _timerButton = new Button();
            _startCancelButton = new Button();
            _uut = new Door();

            _output = Substitute.For<IOutput>();
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _timer = new Timer();
            _light = new Light(_output);
            _cookController = new CookController(_timer, _display, _powerTube, _userInterface);
            _userInterface = new UserInterface(_powerButton, _timerButton, _startCancelButton,
                _uut, _display, _light, _cookController);
        }

        [Test]
        public void OnDoorOpened_StateREADY_OutputsLightON()
        {
            _uut.Open();

            _output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("on")));
        }

        [Test]
        public void OnDoorOpened_StateSETPOWER_OutputsLightON()
        {
            _powerButton.Press();
            _uut.Open();

            _output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("on")));
        }

        [Test]
        public void OnDoorOpened_StateSETPOWER_OutputsDisplayCLeared()
        {
            _powerButton.Press();
            _uut.Open();

            _output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("cleared")));
        }

        [Test]
        public void OnDoorOpened_StateSETTIME_OutputsLightON()
        {
            _powerButton.Press();
            _timerButton.Press();
            _uut.Open();

            _output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("on")));
        }

        [Test]
        public void OnDoorOpened_StateSETTIME_OutputsDisplayCLeared()
        {
            _powerButton.Press();
            _timerButton.Press();
            _uut.Open();

            _output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("cleared")));
        }

        [Test]
        public void OnDoorOpened_StateCOOKING_OutputsPwrTubeTurnedOff()
        {
            _powerButton.Press();
            _timerButton.Press();
            _startCancelButton.Press();
            _uut.Open();

            _output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("powertube")));
        }

        [Test]
        public void OnDoorOpened_StateCOOKING_OutputsDisplayCleared()
        {
            _powerButton.Press();
            _timerButton.Press();
            _startCancelButton.Press();
            _uut.Open();

            _output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("cleared")));
        }

        [Test]
        public void OnDoorClosed_OutputsLightOff()
        {
            _uut.Open();
            _uut.Close();

            _output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("off")));
        }
    }
}