using Assets.Scripts.Helpers;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class InventoryComponent : MonoBehaviour
    {
        public InventoryMenuComponent _inventoryMenu;
        public List<ItemComponent> _items = new List<ItemComponent>();

        public ItemComponent GetDefaultItem() => (_items.Count > 0) ? _items[0] : null;

        public ItemComponent AddContents(ItemComponent item)
        {
            if (!InventoryHelper.IsDuplicate(item, _items))
            {
                _items.Add(item);
                _inventoryMenu.AddItemToInventoryMenu(item);
                //item.transform.SetParent(transform);

                item.ItemGetAnimation();

                return null;
            }
            return item;
        }

        public void Open() => throw new System.NotImplementedException();
    }
}
