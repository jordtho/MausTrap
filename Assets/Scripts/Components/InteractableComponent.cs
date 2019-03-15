using UnityEngine;

namespace Assets.Scripts.Components
{
    public abstract class InteractableComponent : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Sprite _defaultSprite;
        public Sprite _alternateSprite;

        public string _dialog;
        public Item _item;

        public string Dialog { get => _dialog; set => _dialog = value; }
        public Item Item { get => _item; set => _item = value; }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _defaultSprite = _spriteRenderer.sprite;
        }

        public abstract void OnInteract(PlayerComponent player, InventoryComponent inventory);

        public void UseAlternateSprite() => _spriteRenderer.sprite = _alternateSprite;

        public void ReplaceDefaultSprite() => _spriteRenderer.sprite = _defaultSprite;
    }
}
