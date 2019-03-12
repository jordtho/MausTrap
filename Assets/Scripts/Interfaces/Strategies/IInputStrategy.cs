using UnityEngine;

namespace Assets.Scripts.Interfaces.Strategies
{
    public interface IInputStrategy
    {
        void ProcessDirectionalInput(Vector2 directionalInputs);
        void ProcessAButtonInput();
        void ProcessBButtonInput();
        void ProcessXButtonInput();
        void ProcessYButtonInput();
        void ProcessLButtonInput();
        void ProcessRButtonInput();
        void ProcessStartButtonInput();
    }
}
