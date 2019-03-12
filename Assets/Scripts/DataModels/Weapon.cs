using Assets.Scripts.Interfaces;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.DataModels
{
    public abstract class Weapon : Item, IWeapon
    {
        public IDamage Damage { get; set; }
        public int Knockback { get; set; }

        public abstract void OnTriggerEnter2D(Collider2D other);
    }
}
