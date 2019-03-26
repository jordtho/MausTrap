using UnityEngine;

namespace Assets.Scripts.Components
{
    public abstract class InteractableComponent : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Sprite _defaultSprite;
        public Sprite _alternateSprite;

        public string _dialog;
        public ItemComponent _item;

        public string Dialog { get => _dialog; set => _dialog = value; }
        public ItemComponent Item { get => _item; set => _item = value; }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _defaultSprite = _spriteRenderer.sprite;

            var items = GetComponentsInChildren<ItemComponent>();
            if (items.Length > 0)
            {
                if (Item == null) { Item = items[0]; }
                Item.SpriteRenderer = Item.GetComponent<SpriteRenderer>();
                Item.SpriteRenderer.enabled = false;
            }
        }

        public abstract void OnInteract(PlayerComponent player, InventoryComponent inventory);

        public void UseAlternateSprite() => _spriteRenderer.sprite = _alternateSprite;

        public void ReplaceDefaultSprite() => _spriteRenderer.sprite = _defaultSprite;
    }
}
