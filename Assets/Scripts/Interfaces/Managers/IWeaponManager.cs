using UnityEngine;

namespace Assets.Scripts.Interfaces.Managers
{
    public interface IWeaponManager
    {
        IWeapon BuildWeaponObject(IWeapon weapon);
    }
}
