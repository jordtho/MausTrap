using Assets.Scripts.Interfaces.Maps.InputDevices;

namespace Assets.Scripts.Maps.InputDevices
{
    class XboxControllerInputMap : IXboxControllerInputMap
    {
        public string[] JoystickLeft => new string[] { "Horizontal", "Vertical" };
        public string[] JoystickRight => new string[] { "Horizontal", "Vertical" };
        public string[] DPad => new string[] { "Horizontal", "Vertical" };
        public string JoystickLeftButton => "ButtonJoystick1";
        public string JoystickRightButton => "ButtonJoystick2";
        public string AButton => "ButtonA";
        public string BButton => "ButtonB";
        public string XButton => "ButtonX";
        public string YButton => "ButtonY";
        public string StartButton => "ButtonStart";
        public string BackButton => "ButtonBack";
        public string HomeButton => "ButtonHome";
        public string LBButton => "ButtonL1";
        public string LTButton => "ButtonL2";
        public string RBButton => "ButtonR1";
        public string RTButton => "ButtonR2";
    }
}
