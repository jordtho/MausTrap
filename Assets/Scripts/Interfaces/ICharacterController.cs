using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface ICharacterController
    {
        bool ControlLoss { get; set; }
        float MoveSpeed { get; set; }
        float MoveSpeedMultiplier { get; set; }
        Vector2 CharacterFacingVector { get; set; }

        void Move(Vector2 inputDirection);
        void UseItem();
    }
}
