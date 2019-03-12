using UnityEngine;

namespace Assets.Scripts.Interfaces.Managers
{
    public interface IInputManager
    {
        void GetDirectionalInput();
        void GetAButtonInput();
        void GetBButtonInput();
        void GetXButtonInput();
        void GetYButtonInput();
        void GetLButtonInput();
        void GetRButtonInput();
        void GetStartButtonInput();
    }
}
