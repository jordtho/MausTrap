using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface I2DObjectView
    {
        Rigidbody2D Rigidbody { get; set; }
        Animator Animator { get; set; }
        Collider2D Collider { get; set; }
    }
}
