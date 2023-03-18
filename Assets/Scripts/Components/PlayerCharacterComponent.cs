using Assets.Scripts.Components;
using Assets.Scripts.Components.Items;
using Assets.Scripts.Managers;
using Assets.Scripts.Models;
using System.Collections;
using UnityEngine;

public class PlayerCharacterComponent : CharacterComponent
{
    public HeadsUpDisplayComponent _HUD;
    public PlayerComponent _player;
    public ItemComponent _equippedItem;
    public InventoryComponent _inventory;
    public WeaponComponent _weapon = null;

    public PlayerComponent Player { get => _player; }
    public InventoryComponent Inventory { get => _inventory; }

    void Awake()
    {
        _animator = GetComponent<Animator>();
        EquipItem(_inventory.GetDefaultItem());
        _HUD.HealthBarComponent.UpdateHealth(_health);
        _HUD.MoneyComponent.UpdateMoney(_money);
    }

    public void EquipItem(ItemComponent item)
    {
        _equippedItem = item;
        _HUD.EquippedItemComponent.UpdateEquippedItem(item);
    }

    public override void RestoreHealth(int value)
    {
        base.RestoreHealth(value);
        _HUD.HealthBarComponent.UpdateHealth(_health);
    }

    public override void ReceiveAttack(AttackModel attack)
    {
        base.ReceiveAttack(attack);
        _HUD.HealthBarComponent.UpdateHealth(_health);
    }

    public void Interact(PlayerComponent player) => ParseInteraction()?.OnInteract(player, _inventory);

    public InteractableComponent ParseInteraction()
    {
        var start = _rigidbody.position + _collider.offset + (_facing * .625f) + (new Vector2(_facing.y, _facing.x) * .375f);
        var end = _rigidbody.position + _collider.offset + (_facing * .625f) - (new Vector2(_facing.y, _facing.x) * .375f);

        if (GameManager.Instance.DebugMode) { Debug.DrawLine(start, end, Color.green); }
        RaycastHit2D hit = Physics2D.Linecast(start, end, 1 << LayerMask.NameToLayer("Object"));

        return hit.collider?.GetComponent<InteractableComponent>();
    }

    public void Attack()
    {
        _weapon.Use(this);

        if (!GetComponent<Animator>().GetBool("IsAttacking"))
        {
            StartCoroutine(IAttack());
        }
    }

    public void UseItem() => throw new System.NotImplementedException();

    private IEnumerator IAttack()
    {
        _animator.SetBool("IsAttacking", true);
        var multiplier = _moveSpeedMultiplier;
        _moveSpeedMultiplier = 0.0f;

        yield return new WaitForSeconds(1f / 3f); // TODO: Replace with anim.ClipLength if possible: will need to figure out how to access the clip from the Animator

        _animator.SetBool("IsAttacking", false);
        _moveSpeedMultiplier = multiplier;
    }
}