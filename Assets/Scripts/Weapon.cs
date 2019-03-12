using UnityEngine;
using System.Collections;
namespace Assets.Scripts.Old
{
    public class Weapon : Item
    {

        public int m_WeaponDamage = 5;
        public int m_WeaponKnockback = 20;
        public bool attacking;
        public Transform m_ParentTransform;

        IEnumerator SwingWeaponCoroutine;

        void Awake() { }

        public void OnTriggerEnter2D(Collider2D other)
        {

            Character _TargetCharacter = other.GetComponent<Character>();
            if (_TargetCharacter != null)
            {

                _TargetCharacter.TakeDamage(m_WeaponDamage);
                Vector2 _KnockbackDirection = (other.transform.position - GetComponentInParent<Character>().transform.position).normalized;
                _TargetCharacter.Knockback(_KnockbackDirection, m_WeaponKnockback);
            }
        }

        public override void Use(Character character)
        {

            if (attacking)
            {
                StopCoroutine(SwingWeaponCoroutine);
            }
            else
            {
                gameObject.SetActive(true);
            }

            SwingWeaponCoroutine = ISwingWeapon(character);
            StartCoroutine(SwingWeaponCoroutine);
        }

        IEnumerator ISwingWeapon(Character character)
        {

            attacking = true;

            float time = 0.25f;
            float originalTime = time;

            while (time > 0.0f)
            {

                m_ParentTransform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, character.m_CharacterRotation - 90f), Quaternion.Euler(0, 0, character.m_CharacterRotation + 60f), 1 - (time / originalTime));
                time -= Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            gameObject.SetActive(false);
            attacking = false;
        }
    }
}