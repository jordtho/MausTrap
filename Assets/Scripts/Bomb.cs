using System.Collections;
using UnityEngine;

public class Bomb : Item {

    public float m_DurationBeforeFlash, m_DurationFlash, m_DurationExplosion = 1f;
    public float m_ExplosionKnockbackForce = 30.0f;

    public override void Use() {
        ShowSprite();
        StartDetonation();
    }

    private void StartDetonation() {
        StartCoroutine(IBombAnimation());
    }

    private void ProcessDetonation() {

        Collider2D[] _CollidersInExplosion = Physics2D.OverlapCircleAll(transform.position, m_Range);

        for(int i = 0; i < _CollidersInExplosion.Length; i++) {

            if(_CollidersInExplosion[i].GetComponent<Character>()) {

                Character _CharacterTarget = _CollidersInExplosion[i].GetComponent<Character>();
                float _Distance = Vector2.Distance(_CharacterTarget.transform.position, transform.position);
                Vector2 _KnockbackDirection = (_CharacterTarget.transform.position - transform.position).normalized;

                Debug.Log("Bomb hit: " + _CharacterTarget.name);
                _CharacterTarget.TakeDamage((int)(((m_Range - _Distance) / m_Range) * m_Damage));
                _CharacterTarget.Knockback(_KnockbackDirection, ((m_Range - _Distance) / m_Range) * m_ExplosionKnockbackForce);
            }
        }
    }

    IEnumerator IBombAnimation() {

        yield return new WaitForSeconds(m_DurationBeforeFlash);
        GetComponent<Animator>().SetInteger("state", 1); //Flashing Animation
        yield return new WaitForSeconds(m_DurationFlash);
        GetComponent<Animator>().SetInteger("state", 2); //Boom! Animation
        ProcessDetonation();
        yield return new WaitForSeconds(m_DurationExplosion);

        Destroy(gameObject); //Remove object after it explodes
    }	
}
