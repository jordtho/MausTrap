using UnityEngine;

namespace Assets.Scripts.Components
{
    public abstract class WeaponComponent : ItemComponent
    {
        public int _damage;

        public abstract void Attack(Vector2 direction);
    }
}
