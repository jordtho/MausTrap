namespace Assets.Scripts.Interfaces.Maps.InputDevices
{
    public interface IInputDeviceMap
    {
        string[] JoystickLeft { get; }
        string AButton { get; }
        string BButton { get; }
        string XButton { get; }
        string YButton { get; }
        string StartButton { get; }
    }
}
