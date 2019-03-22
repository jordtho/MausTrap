using Assets.Scripts.Helpers;
using Assets.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterComponent : MonoBehaviour
{
    public Text _queueText = null;

    // View
    public Animator _animator;
    public Collider2D _collider;
    public Rigidbody2D _rigidbody;
    public SpriteRenderer _spriteRenderer;

    public Vector2 _facing;
    public float _rotation;
    public float _moveSpeed;
    public float _moveSpeedMultiplier;

    public bool _invincible;
    public int _maxHealth;
    public int _health;
    public int _defense;
    public int _money;

    public List<InputDirection> _inputs;
    public Vector2 _curDir = Vector2.zero;

    // Model
    public bool _controlLoss = false;

    public void Move(Vector2 inputs)
    {
        if (inputs.x != _curDir.x)
        {
            if (_curDir.x < 0) { _inputs.Remove(InputDirection.LEFT); }
            if (_curDir.x > 0) { _inputs.Remove(InputDirection.RIGHT); }

            _curDir.x = inputs.x;

            if (_curDir.x < 0) { _inputs.Add(InputDirection.LEFT); }
            if (_curDir.x > 0) { _inputs.Add(InputDirection.RIGHT); }
        }

        if (inputs.y != _curDir.y)
        {
            if (_curDir.y < 0) { _inputs.Remove(InputDirection.DOWN); }
            if (_curDir.y > 0) { _inputs.Remove(InputDirection.UP); }

            _curDir.y = inputs.y;

            if (_curDir.y < 0) { _inputs.Add(InputDirection.DOWN); }
            if (_curDir.y > 0) { _inputs.Add(InputDirection.UP); }
        }

        if (_inputs.Count > 0) { UpdateFacing(_inputs[0]); }

        if (!_controlLoss)
        {
            if (inputs == Vector2.zero && _animator.GetBool("moving")) { _animator.SetBool("moving", false); }
            if (inputs != Vector2.zero && !_animator.GetBool("moving")) { _animator.SetBool("moving", true); }

            _rigidbody.velocity = inputs.normalized * _moveSpeed * _moveSpeedMultiplier;
        }

        // Debug
        if (_queueText != null) { UpdateQueueText(); }
    }

    public virtual void RestoreHealth(int value) => _health += (_health + value > _maxHealth) ? 0 : value;

    public virtual void TakeDamage(int damage)
    {
        if (!_invincible)
        {
            InvincibilityFrames();
            int trueDamage = (damage - _defense >= 1) ? damage - _defense : 1;
            _health = (_health - trueDamage >= 0) ? _health - trueDamage : 0;

            if (_health <= 0 && !_animator.GetBool("dead"))
            {
                _animator.SetBool("dead", true);
                Die();
            }
        }
    }

    public void UpdateFacing(InputDirection direction)
    {
        _facing = CharacterHelper.GetFacing(direction);
        _rotation = CharacterHelper.GetRotation(direction);

        _animator.SetFloat("input_x", _facing.x);
        _animator.SetFloat("input_y", _facing.y);
    }

    public void UpdateQueueText()
    {
        _queueText.text = "";
        foreach (InputDirection input in _inputs) { _queueText.text += input.ToString() + "\n"; }
    }

    private void InvincibilityFrames()
    {
        _invincible = true;
        StartCoroutine(IInvincibilityFrames());
    }

    private IEnumerator IInvincibilityFrames()
    {
        float _time = 0f;
        int _i = 1;

        while (_time < GameManager.Instance.m_IFramesDuration)
        {
            _spriteRenderer.enabled = _i > GameManager.Instance.m_IFrameFlickerRate ? true : false;
            if (_i < (11 - GameManager.Instance.m_IFrameFlickerRate) * 2) { ++_i; } else { _i = 1; }
            _time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        _spriteRenderer.enabled = true;
        _invincible = false;
    }

    public void Knockback(Vector2 direction, float force) => StartCoroutine(IKnockback(direction, force));

    private IEnumerator IKnockback(Vector2 direction, float force)
    {
        _controlLoss = true;
        _rigidbody.velocity = direction * force;
        yield return new WaitForSeconds(0.1f);
        _controlLoss = false;
    }

    private void Die() => StartCoroutine(IDie());

    private IEnumerator IDie()
    {
        _animator.SetBool("dead", true);
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}