using Assets.Scripts.Components;
using Assets.Scripts.Old;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Managers;

public class PlayerCharacterComponent : CharacterComponent
{
    public AnimHelper m_AnimationHelper;

    // View
    public Text _equippedItemQuantityTextComponent;
    public HealthBar _healthBarComponent;

    // Model
    public Item _equippedItem;
    public InventoryComponent _inventory;
    public Weapon _weapon = null;

    void Awake()
    {
        EquipItem(_inventory.GetDefaultItem());
        UpdateHealth(_health);
    }

    public void OnTriggerStay2D(Collider2D collider)
    {
        //if (collider.GetComponentInParent<Enemy>()) { collider.GetComponentInParent<Enemy>().AcquireTarget(this); }
    }

    public void OnCollisionStay2D(Collision2D coll)
    {

        if (coll.collider.GetComponent<Enemy>() && !_invincible)
        {
            Knockback(coll.contacts[0].normal, coll.collider.GetComponent<Enemy>().m_CollisionKnockbackForce);
            TakeDamage(coll.collider.GetComponent<Enemy>().m_CollisionDamage);
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        //if (collider.GetComponent<Pickup>() && collider.GetComponent<Pickup>().m_CanInteract)
        //{
        //    collider.GetComponent<Pickup>().PickUpItem(this);
        //}
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        //if (coll.collider.GetComponent<Pickup>() && coll.collider.GetComponent<Pickup>().m_CanInteract)
        //{
        //    coll.collider.GetComponent<Pickup>().PickUpItem(this);
        //}
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.GetComponent<Pickup>())
        {
            collider.GetComponent<Pickup>().m_CanInteract = true;
        }

        //if (collider.GetComponentInParent<Enemy>())
        //{
        //    collider.GetComponentInParent<Enemy>().DisengageTarget(this);
        //}
    }

    public void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.collider.GetComponent<Pickup>())
        {
            coll.collider.GetComponent<Pickup>().m_CanInteract = true;
        }
    }

    public void EquipItem(Item item)
    {
        _equippedItem = item;
        UpdateItemQuantityText(item);
    }

    public void UpdateItemQuantityText(Item item) => _equippedItemQuantityTextComponent.text = item.m_Quantity.ToString();

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        UpdateHealth(_health);
    }

    public void UpdateHealth(int value) => _healthBarComponent.SetCurrentHealth(value);

    public void Interact(PlayerComponent player) => ParseInteraction()?.OnInteract(player, _inventory);

    public InteractableComponent ParseInteraction()
    {
        var start = _rigidbody.position + _collider.offset + (_facing * .625f) + (new Vector2(_facing.y, _facing.x) * .375f);
        var end = _rigidbody.position + _collider.offset + (_facing * .625f) - (new Vector2(_facing.y, _facing.x) * .375f);

        if (GameManager.Instance.m_DebugMode) { Debug.DrawLine(start, end, Color.green); }

        RaycastHit2D hit = Physics2D.Linecast(start, end, 1 << LayerMask.NameToLayer("Object"));
        InteractableComponent interactable = null;

        if (hit.collider != null)
        {
            if (GameManager.Instance.m_DebugMode) { Debug.Log($"Interaction with { hit.collider.gameObject.name } was successful!"); }

            interactable = hit.collider.GetComponent<InteractableComponent>();
        }

        return interactable;
    }

    public void Attack()
    {
        _weapon.Use();
        if (!GetComponent<Animator>().GetBool("attacking")) { StartCoroutine(IAttack()); }
    }

    public void UseItem()
    { //Tied to pressing Y Button

        //Use Item
        GetComponent<Player>().m_EquippedItem.UseItem();

        //Animation
    }

    public void OpenMap()
    { //Tied to pressing X Button
        //Open Map
        //Close Map
    }

    public void OpenInventory()
    {
        MenuManager.Instance.OpenMenu(GetComponent<Player>().m_Inventory.m_InventoryMenu);
    }

    private IEnumerator IAttack()
    {

        GetComponent<Animator>().SetBool("attacking", true);

        float m_OriginalMovespeed = GetComponent<Player>().m_MoveSpeed;

        GetComponent<Player>().m_MoveSpeed = 0.0f * GetComponent<Player>().m_MoveSpeed;

        //GameManager.instance.m_GameStateStack.Add(GameState.LOCKED);

        yield return new WaitForSeconds(1f / 3f); //Replace with anim.ClipLength if possible: will need to figure out how to access the clip from the Animator

        //GameManager.instance.m_GameStateStack.Remove(GameState.LOCKED);

        GetComponent<Animator>().SetBool("attacking", false);

        GetComponent<Player>().m_MoveSpeed = m_OriginalMovespeed;
    }
}