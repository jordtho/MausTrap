using Assets.Scripts.Helpers;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class InventoryComponent : MonoBehaviour
    {
        public List<Item> _items = new List<Item>();

        public Item GetDefaultItem() => (_items.Count > 0) ? _items[0] : null;

        public Item AddContents(Item item)
        {
            if (!InventoryHelper.IsDuplicate(item, _items))
            {
                _items.Add(item);
                item.ItemGetAnimation();

                return null;
            }
            return item;
        }
    }
}
