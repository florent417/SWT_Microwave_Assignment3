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
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Test.Integration
{

    [TestFixture]
    class _2nd_Integration_Userinterface
    {
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private IDoor _door;

        private IUserInterface _uut;
        private ICookController _cookController;
        private ILight _light;
        private ITimer _timer;
        private IDisplay _display;
        private IPowerTube _powerTube;
        private IOutput _output;
        

        [SetUp]
        public void SetUp()
        {
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _startCancelButton = Substitute.For<IButton>();
            _door = Substitute.For<IDoor>();

            _output = Substitute.For<IOutput>();
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _timer = new Timer();
            _light = new Light(_output);
            _cookController = new CookController(_timer, _display, _powerTube, _uut);
            _uut = new UserInterface(_powerButton, _timeButton, _startCancelButton, 
                _door, _display, _light, _cookController);
        }

        #region OnPwrBtnPressed tests

        [Test]
        public void OnPowerButtonPressed_StartOK()
        {
            _uut.OnPowerPressed(null, null);
            _output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("display shows: 50 w")));
        }

        [Test]
        public void OnPowerButtonPressed_OutputPwr100()
        {
            _uut.OnPowerPressed(null, null);
            _uut.OnPowerPressed(null, null);
            _output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("display shows: 100 w")));
        }

        [Test]
        public void OnPowerButtonPressed_Pwr100_CorrectOutput()
        {
            _uut.OnPowerPressed(null, null);
            _uut.OnPowerPressed(null, null);
            _output.DidNotReceive().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("display shows: 150 w")));
        }

        [Test]
        public void OnPowerButtonPressed_PwrIncreaseAbove700_Outputs50W()
        {
            for (int i = 50; i <= 700; i += 50)
            {
                _uut.OnPowerPressed(null, null);
            }
            _uut.OnPowerPressed(null, null);

            _output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("50 w")));
        }

        [Test]
        public void OnPowerButtonPressed_PwrIncreaseAbove700_OutputIsCorrect()
        {
            for (int i = 50; i <= 700; i += 50)
            {
                _uut.OnPowerPressed(null, null);
            }
            _uut.OnPowerPressed(null, null);

            _output.DidNotReceive().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("750 w")));
        }

        #endregion

        #region TimeBtn tests
        [Test]
        public void SetPower_TimeButton_OutputsTime1()
        {
            // Also checks if TimeButton is subscribed
            //_powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            //// Now in SetPower
            //_timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _uut.OnPowerPressed(null, null);
            _uut.OnTimePressed(null, null);

            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("01:00")));
        }

        [Test]
        public void SetPower_2TimeButton_OutputsTime2()
        {
            //_powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            //// Now in SetPower
            //_timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            //_timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _uut.OnPowerPressed(null, null);
            _uut.OnTimePressed(null, null);
            _uut.OnTimePressed(null, null);

            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("02:00")));
        }

        [Test]
        public void SetPower_2TimeButton_OutputIsCorrect()
        {
            //_powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            //// Now in SetPower
            //_timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            //_timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _uut.OnPowerPressed(null, null);
            _uut.OnTimePressed(null, null);
            _uut.OnTimePressed(null, null);

            _output.DidNotReceive().OutputLine(Arg.Is<string>(str => str.Contains("03:00")));
        }


        #endregion

        #region OnStartCancelPressed

        #region State=SETPOWER

        [Test]
        public void OnStartCancelPressed_StateSETPOWER_OutputsClearedDisplay()
        {
            _uut.OnPowerPressed(null, null);
            _uut.OnStartCancelPressed(null, null);

            _output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("cleared")));
        }

        [Test]
        public void OnStartCancelPressed_StateSETPOWER_OutputsLightTurnedOn()
        {
            _uut.OnPowerPressed(null, null);
            _uut.OnStartCancelPressed(null, null);

            _output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("on")));
        }

        [Test]
        public void OnStartCancelPressed_StateSETPOWER_OutputsPwrTubePower()
        {
            _uut.OnPowerPressed(null, null);
            _uut.OnStartCancelPressed(null, null);

            _output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("powertube")));
        }

        /*
         // Dont know how to test showTime
        [Test]
        public void OnStartCancelPressed_StateSETPOWER_OutputsTimeRemaining()
        {
            _uut.OnPowerPressed(null, null);
            _uut.OnStartCancelPressed(null, null);

            Thread.Sleep(500);

            

            
            _output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("00:40")));
        }
        */

        #endregion

        #region State=SETTIME



        #endregion


        #endregion


    }
}
