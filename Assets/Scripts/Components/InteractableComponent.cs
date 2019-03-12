using UnityEngine;

namespace Assets.Scripts.Components
{
    public class InteractableComponent : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        public Sprite _alternateSprite;

        public string _dialog;
        public Item _item;

        public string Dialog { get => _dialog; set => _dialog = value; }
        public Item Item { get => _item; set => _item = value; }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void UseAlternateSprite() => _spriteRenderer.sprite = _alternateSprite;
    }
}
