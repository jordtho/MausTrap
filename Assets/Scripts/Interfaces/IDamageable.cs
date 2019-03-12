using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IDamageable
    {
        bool Invulnerable { get; set; }
        int MaxHealth { get; set; }
        int CurrentHealth { get; set; }
        int Armor { get; set; }

        void TakeDamage(int damageAmount);

        void Knockback(Vector3 normalizedDirection,int force);

        void HealDamage(int healAmount);

        void Die();
    }
}
