using Assets.Scripts.Components.Items;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public abstract class InteractableComponent : MonoBehaviour
    {
        #region Fields

        protected SpriteRenderer _spriteRenderer;

        [SerializeField] protected string _dialog;
        [SerializeField] protected ItemComponent _item;

        #endregion

        #region Unity Awake

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            var item = GetComponentInChildren<ItemComponent>();

            if (_item == null && item != null)
            {
                PutItem(item);
            }
        }

        #endregion

        #region Methods

        public abstract void OnInteract(PlayerComponent player, InventoryComponent inventory);

        public void PutItem(ItemComponent item)
        {
            if (item == null)
            {
                Debug.LogWarning("Will not put null item. Use TakeItem() to nullify item.", this);
                return;
            }

            _item = item;
            _item.transform.SetParent(this.transform);
            _item.Hide();
        }

        public ItemComponent TakeItem()
        {
            var item = _item;
            _item = null;
            return item;
        }

        public void RemoveContents()
        {
            if (_item != null)
            {
                Destroy(_item);
            }
        }

        #endregion
    }
}
