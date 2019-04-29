using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace ClassLibrary2
{
    [TestFixture]
    class _5th_Integration_StartCancelButton
    {
        private IButton _powerButton;
        private IButton _timerButton;
        private IButton _uut;
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
            _timerButton = new Button();
            _uut = new Button();
            _door = Substitute.For<IDoor>();

            _output = Substitute.For<IOutput>();
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _timer = new MicrowaveOvenClasses.Boundary.Timer();
            _light = new Light(_output);
            _cookController = new CookController(_timer, _display, _powerTube, _userInterface);
            _userInterface = new UserInterface(_powerButton, _timerButton, _uut,
            _door, _display, _light, _cookController);
        }

        [Test]
        public void OnStartCancelPressed_StateSETPOWER_OutputsClearedDisplay()
        {
            _powerButton.Press();
            _uut.Press();

            _output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("cleared")));
        }

        [Test]
        public void OnStartCancelPressed_StateSETTIME_OutputsClearedDisplay()
        {
            _powerButton.Press();
            _timerButton.Press();
            _uut.Press();

            _output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("cleared")));
        }

        [Test]
        public void OnStartCancelPressed_StateSETTime_OutputsLightTurnedOn()
        {
            _powerButton.Press();
            _timerButton.Press();
            _uut.Press();

            _output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("on")));
        }

        [Test]
        public void OnStartCancelPressed_StateSETTime_OutputsPwrTubePower()
        {
            _powerButton.Press();
            _timerButton.Press();
            _uut.Press();

            _output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("powertube")));
        }

        [Test]
        public void OnStartCancelPressed_StateCOOKING_OutputsPwrTubeOff()
        {
            _powerButton.Press();
            _timerButton.Press();
            _uut.Press();
            _uut.Press();

            _output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("powertube turned off")));
        }

        [Test]
        public void OnStartCancelPressed_StateCOOKING_OutputsLightOff()
        {
            _powerButton.Press();
            _timerButton.Press();
            _uut.Press();
            _uut.Press();

            _output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("light is turned off")));
        }

        [Test]
        public void OnStartCancelPressed_StateCOOKING_OutputsDisplayCleared()
        {
            _powerButton.Press();
            _timerButton.Press();
            _uut.Press();
            _uut.Press();

            _output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("cleared")));
        }

        // Dont know how to test showTime
        [Test]
        public void OnStartCancelPressed_StateCOOKING_OutputsTimeRemaining()
        {
            _powerButton.Press();
            _timerButton.Press();
            _uut.Press();

            // Wait for 10 secs
            Thread.Sleep(10200);

            _output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("00:50")));
        }

        // Dont know how to test showTime
        [Test]
        public void OnStartCancelPressed_StateCOOKING_OutputsPwrTubeIsOFF()
        {
            _powerButton.Press();
            _timerButton.Press();
            _uut.Press();

            Thread.Sleep(60000);

            _output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("powertube")));
            _output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("display")));
            _output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("light")));
        }
    }
}
