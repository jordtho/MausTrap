using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class InventoryComponent : MonoBehaviour
    {
        private List<Item> _items = new List<Item>();

        public Item GetDefaultItem() => (_items.Count > 0) ? _items[0] : null;

        public Item AddContents(Item item)
        {
            if (!_items.Contains(item))
            {
                _items.Add(item);
                return null;
            }

            return item;
        }
    }
}
