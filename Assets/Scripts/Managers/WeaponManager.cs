using Assets.Scripts.Interfaces;
using Assets.Scripts.Interfaces.Managers;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class WeaponManager : IWeaponManager
    {
        public IWeapon BuildWeaponObject(IWeapon weapon)
        {
            GameObject obj = new GameObject(weapon.Name);
            IWeapon _weapon = obj.AddComponent(typeof(IWeapon)) as IWeapon;

            _weapon.Name = weapon.Name;
            _weapon.Damage = weapon.Damage;
            _weapon.Knockback = weapon.Knockback;

            return _weapon;
        }
    }
}
