using Assets.Scripts.Interfaces;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.DataModels
{
    public abstract class Character : MonoBehaviour, ICharacter, ICharacterController, I2DObjectView, IAttacker
    {
        public Rigidbody2D Rigidbody { get; set; }
        public Animator Animator { get; set; }
        public Collider2D Collider { get; set; }
        public string Name { get; set; }
        public IItem EquippedItem { get; set; }
        public bool Invulnerable { get; set; }
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int Armor { get; set; }
        public bool ControlLoss { get; set; }
        public float MoveSpeed { get; set; }
        public float MoveSpeedMultiplier { get; set; }
        public IWeapon Weapon { get; set; }
        public Vector2 CharacterFacingVector { get; set; }

        public void Attack()
        {
            Weapon.Use();
        }
        public void Die()
        {
            ControlLoss = true;
            Animator.SetBool("Die", true);
        }
        public void HealDamage(int healAmount)
        {
            CurrentHealth = (CurrentHealth + healAmount < MaxHealth) ? (CurrentHealth + healAmount) : MaxHealth;
        }
        public void Knockback(Vector3 normalizedDirection, int force)
        {
            StartCoroutine(KnockbackCoroutine(normalizedDirection, force));
        }
        private IEnumerator KnockbackCoroutine(Vector3 normalizedDirection, int force)
        {
            ControlLoss = true;
            Rigidbody.velocity = normalizedDirection * force;
            yield return new WaitForSeconds(0.1f);
            ControlLoss = false;
        }
        public void TakeDamage(int damageAmount)
        {
            CurrentHealth -= damageAmount;
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                Die();
            }
        }
        public void Move(Vector2 inputDirection)
        {
            if (!ControlLoss)
            {
                if (inputDirection == Vector2.zero && Animator.GetBool("moving")) { Animator.SetBool("moving", false); }
                if (inputDirection != Vector2.zero && !Animator.GetBool("moving")) { Animator.SetBool("moving", true); }

                Rigidbody.velocity = inputDirection.normalized * MoveSpeed * MoveSpeedMultiplier;
            }
        }
        public void UseItem() => EquippedItem.Use();
        public void Equip(IItem item) => EquippedItem = item;
    }
}
