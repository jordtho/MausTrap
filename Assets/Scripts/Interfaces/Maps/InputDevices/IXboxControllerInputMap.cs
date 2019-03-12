using UnityEngine;

namespace Assets.Scripts.Interfaces.Maps.InputDevices
{
    public interface IXboxControllerInputMap : IInputDeviceMap
    {
        string[] JoystickRight { get; }
        string[] DPad { get; }
        string JoystickLeftButton { get; }
        string JoystickRightButton { get; }
        string BackButton { get; }
        string HomeButton { get; }
        string LBButton { get; }
        string LTButton { get; }
        string RBButton { get; }
        string RTButton { get; }
    }
}
