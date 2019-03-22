using UnityEngine;

namespace Assets.Scripts.Components
{
    public class ItemMenuOptionComponent : MenuOptionComponent
    {
        public ItemComponent _item;

        public ItemComponent Item { get => _item; set => _item = value; }
        public SpriteRenderer SpriteRenderer { get; set; }

        private void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        public override void OnSelect() => throw new System.NotImplementedException();
    }
}