using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;
using NSubstitute;

namespace ClassLibrary2
{
    class _3rd_integration_powerbutton
    {
        private IButton _uut;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private IDoor _door;

        private IUserInterface _ui;
        private ICookController _cookController;
        private ILight _light;
        private ITimer _timer;
        private IDisplay _display;
        private IPowerTube _powerTube;
        private IOutput _output;

        [SetUp]
        public void SetUp()
        {
            _uut = new Button();
            _timeButton = Substitute.For<IButton>();
            _startCancelButton = Substitute.For<IButton>();
            _door = Substitute.For<IDoor>();
            _output = Substitute.For<IOutput>();
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _timer = new Timer();
            _light = new Light(_output);
            _cookController = new CookController(_timer, _display, _powerTube, _ui);
            _ui = new UserInterface(_uut, _timeButton, _startCancelButton,
                _door, _display, _light, _cookController);
        }

        [Test]
        public void OnPowerButtonPressed_StartOK()
        {
            _uut.Press();
            _output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("display shows: 50 w")));
        }

        [Test]
        public void OnPowerButtonPressed_OutputPwr100()
        {
            _uut.Press();
            _uut.Press();
            _output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("display shows: 100 w")));
        }

        [Test]
        public void OnPowerButtonPressed_Pwr100_CorrectOutput()
        {
            _uut.Press();
            _uut.Press();
            _output.DidNotReceive().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("display shows: 150 w")));
        }

        [Test]
        public void OnPowerButtonPressed_PwrIncreaseAbove700_Outputs50W()
        {
            for (int i = 50; i <= 700; i += 50)
            {
                _uut.Press();
            }
            _uut.Press();

            _output.Received(2).OutputLine(Arg.Is<string>(str => str.ToLower().Contains("display shows: 50 w")));
        }

        [Test]
        public void OnPowerButtonPressed_PwrIncreaseAbove700_OutputIsCorrect()
        {
            for (int i = 50; i <= 700; i += 50)
            {
                _uut.Press();
            }
            _uut.Press();

            _output.DidNotReceive().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("750 w")));
        }
    }
}
