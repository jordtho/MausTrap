using Assets.Scripts.Interfaces;
using System;
using UnityEngine;

namespace Assets.Scripts.DataModels
{
    public class Bomb : MonoBehaviour, I2DObjectView, IAreaDamage, IItem
    {
        public int Amount { get; set; }
        public string Name { get; set; }
        public Rigidbody2D Rigidbody { get; set; }
        public Animator Animator { get; set; }
        public Collider2D Collider { get; set; }

        public void DealAreaDamage()
        {
            throw new NotImplementedException();
        }

        public void Detonate()
        {
            throw new NotImplementedException();
        }

        public void Use()
        {
            throw new NotImplementedException();
        }
    }
}
