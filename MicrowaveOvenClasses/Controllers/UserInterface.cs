using System;
using System.Runtime.Serialization;
using MicrowaveOvenClasses.Interfaces;

namespace MicrowaveOvenClasses.Controllers
{
    public class UserInterface : IUserInterface
    {
        private enum States
        {
            READY, SETPOWER, SETTIME, COOKING, DOOROPEN
        }

        private States myState = States.READY;

        private ICookController myCooker;
        private ILight myLight;
        private IDisplay myDisplay;

        private int powerLevel = 50;
        private int time = 1;

        public UserInterface(
            IButton powerButton,
            IButton timeButton,
            IButton startCancelButton,
            IDoor door,
            IDisplay display,
            ILight light,
            ICookController cooker)
        {
            powerButton.Pressed += new EventHandler(OnPowerPressed);
            timeButton.Pressed += new EventHandler(OnTimePressed);
            startCancelButton.Pressed += new EventHandler(OnStartCancelPressed);

            door.Closed += new EventHandler(OnDoorClosed);
            door.Opened += new EventHandler(OnDoorOpened);

            myCooker = cooker;
            myLight = light;
            myDisplay = display;
        }

        public void OnPowerPressed(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.READY:
                    myDisplay.ShowPower(powerLevel);
                    myState = States.SETPOWER;
                    break;
                case States.SETPOWER:
                    powerLevel = (powerLevel >= 700 ? 50 : powerLevel+50);
                    myDisplay.ShowPower(powerLevel);
                    break;
            }
        }

        public void OnTimePressed(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.SETPOWER:
                    myDisplay.ShowTime(time, 0);
                    myState = States.SETTIME;
                    break;
                case States.SETTIME:
                    time += 1;
                    myDisplay.ShowTime(time, 0);
                    break;
            }
        }

        public void OnStartCancelPressed(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.SETPOWER:
                    // this needs to be removed, since we can change
                    // the power in this state in the method 
                    // on power pressed
                    //powerLevel = 50;
                    time = 1;
                    // I think this is a mistake, since on the SD
                    // It says it is supposed to be turned on, and
                    // it also fails with the integration test
                    //myLight.TurnOff();
                    myLight.TurnOn();
                    myDisplay.Clear();
                    // According to the SD as i am reading it, there 
                    // is no possibility to get the state Ready from 
                    // this method. And you should be able to start
                    // cooking even if the state is SETPOWER
                    //myState = States.READY;
                    myCooker.StartCooking((powerLevel / 7), time * 60);
                    myState = States.COOKING;
                    break;
                case States.SETTIME:
                    myDisplay.Clear();
                    myLight.TurnOn();
                    myCooker.StartCooking((powerLevel/7), time*60);
                    myState = States.COOKING;
                    break;
                case States.COOKING:
                    powerLevel = 50;
                    time = 1;
                    myCooker.Stop();
                    myLight.TurnOff();
                    myDisplay.Clear();
                    myState = States.READY;
                    break;
            }
        }

        public void OnDoorOpened(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.READY:
                    myLight.TurnOn();
                    myState = States.DOOROPEN;
                    break;
                case States.SETPOWER:
                    powerLevel = 50;
                    myLight.TurnOn();
                    myDisplay.Clear();
                    myState = States.DOOROPEN;
                    break;
                case States.SETTIME:
                    powerLevel = 50;
                    time = 1;
                    myLight.TurnOn();
                    myDisplay.Clear();
                    myState = States.DOOROPEN;
                    break;
                case States.COOKING:
                    myCooker.Stop();
                    powerLevel = 50;
                    time = 1;
                    myState = States.DOOROPEN;
                    break;
            }
        }

        public void OnDoorClosed(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.DOOROPEN:
                    myLight.TurnOff();
                    myState = States.READY;
                    break;
            }
        }

        public void CookingIsDone()
        {
            switch (myState)
            {
                case States.COOKING:
                    powerLevel = 50;
                    time = 1;
                    myDisplay.Clear();
                    myLight.TurnOff();
                    // Beep 3 times
                    myState = States.READY;
                    break;
            }
        }
    }
}