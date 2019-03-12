using Assets.Scripts.Interfaces.Managers;
using Assets.Scripts.Interfaces.Maps.InputDevices;
using Assets.Scripts.Interfaces.Strategies;
using System;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class InputManager : IInputManager
    {
        IInputDeviceMap inputDeviceMap;
        IInputStrategy inputStrategy;

        public InputManager(IInputDeviceMap inputDeviceMap, IInputStrategy inputStrategy)
        {
            this.inputDeviceMap = inputDeviceMap;
            this.inputStrategy = inputStrategy;
        }

        public void GetAButtonInput()
        {
            if (Input.GetButtonDown(inputDeviceMap.AButton))
            {
                inputStrategy.ProcessAButtonInput();
            }
        }

        public void GetBButtonInput()
        {
            if (Input.GetButtonDown(inputDeviceMap.BButton))
            {
                inputStrategy.ProcessBButtonInput();
            }
        }

        public void GetDirectionalInput()
        {
            inputStrategy.ProcessDirectionalInput(new Vector2(Input.GetAxisRaw(inputDeviceMap.JoystickLeft[0]), Input.GetAxisRaw(inputDeviceMap.JoystickLeft[1])));
        }

        public void GetLButtonInput()
        {
            throw new NotImplementedException();
        }

        public void GetRButtonInput()
        {
            throw new NotImplementedException();
        }

        public void GetStartButtonInput()
        {
            if (Input.GetButtonDown(inputDeviceMap.StartButton))
            {
                inputStrategy.ProcessStartButtonInput();
            }
        }

        public void GetXButtonInput()
        {
            if (Input.GetButtonDown(inputDeviceMap.XButton))
            {
                inputStrategy.ProcessXButtonInput();
            }
        }

        public void GetYButtonInput()
        {
            if (Input.GetButtonDown(inputDeviceMap.YButton))
            {
                inputStrategy.ProcessYButtonInput();
            }
        }
    }
}
