using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface ICharacterView : I2DObjectView
    {
        Vector2 FacingVector { get; set; }
        float Rotation { get; set; }

        void UpdateFacing();
    }
}
