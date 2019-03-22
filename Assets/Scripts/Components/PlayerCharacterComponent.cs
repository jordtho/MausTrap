using Assets.Scripts.Components;
using Assets.Scripts.Managers;
using Assets.Scripts.Old;
using System.Collections;
using UnityEngine;

public class PlayerCharacterComponent : CharacterComponent
{
    // View
    public HeadsUpDisplayComponent _HUD;

    // Model
    public Item _equippedItem;
    public InventoryComponent _inventory;
    public Weapon _weapon = null;

    private Animator Animator { get; set; }

    void Awake()
    {
        Animator = GetComponent<Animator>();
        EquipItem(_inventory.GetDefaultItem());
        _HUD.UpdateHealth(_health);
        _HUD.UpdateMoney(_money);
    }

    public void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.collider.GetComponent<Enemy>() && !_invincible)
        {
            Knockback(coll.contacts[0].normal, coll.collider.GetComponent<Enemy>().m_CollisionKnockbackForce);
            TakeDamage(coll.collider.GetComponent<Enemy>().m_CollisionDamage);
        }
    }

    public void EquipItem(Item item)
    {
        _equippedItem = item;
        _HUD.UpdateEquippedItem(item);
    }

    public override void RestoreHealth(int value)
    {
        base.RestoreHealth(value);
        _HUD.UpdateHealth(_health);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        _HUD.UpdateHealth(_health);
    }

    public void Interact(PlayerComponent player) => ParseInteraction()?.OnInteract(player, _inventory);

    public InteractableComponent ParseInteraction()
    {
        var start = _rigidbody.position + _collider.offset + (_facing * .625f) + (new Vector2(_facing.y, _facing.x) * .375f);
        var end = _rigidbody.position + _collider.offset + (_facing * .625f) - (new Vector2(_facing.y, _facing.x) * .375f);

        if (GameManager.Instance.m_DebugMode) { Debug.DrawLine(start, end, Color.green); }
        RaycastHit2D hit = Physics2D.Linecast(start, end, 1 << LayerMask.NameToLayer("Object"));

        return hit.collider?.GetComponent<InteractableComponent>();
    }

    public void Attack()
    {
        _weapon.Use();
        if (!GetComponent<Animator>().GetBool("attacking")) { StartCoroutine(IAttack()); }
    }

    public void UseItem() => _equippedItem.UseItem();

    public void OpenMap() => throw new System.NotImplementedException();

    public void OpenInventory() => _inventory.Open();

    private IEnumerator IAttack()
    {
        Animator.SetBool("attacking", true);
        var multiplier = _moveSpeedMultiplier;
        _moveSpeedMultiplier = 0.0f;

        yield return new WaitForSeconds(1f / 3f); //TODO: Replace with anim.ClipLength if possible: will need to figure out how to access the clip from the Animator

        Animator.SetBool("attacking", false);
        _moveSpeedMultiplier = multiplier;
    }
}