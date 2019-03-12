using Assets.Scripts.DataModels;
using Assets.Scripts.Interfaces;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class SlashWeapon : Weapon
    {
        public override void OnTriggerEnter2D(Collider2D other)
        {
            IDamageable target = other.GetComponent<IDamageable>();
            if (target != null)
            {
                target.TakeDamage(Damage.Amount);
                target.Knockback((other.transform.position - transform.parent.position).normalized, Knockback);
            }
        }
        protected override IEnumerator UseCoroutine()
        {
            float time = 0.25f;
            float originalTime = time;

            while (time > 0.0f)
            {
                transform.parent.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, transform.parent.rotation.z - 90f), Quaternion.Euler(0, 0, transform.parent.rotation.z + 60f), 1 - (time / originalTime));
                time -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            gameObject.SetActive(false);
        }
    }
}
