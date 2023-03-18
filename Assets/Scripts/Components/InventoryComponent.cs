using Assets.Scripts.Components.Items;
using Assets.Scripts.Enums;
using Assets.Scripts.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class InventoryComponent : MonoBehaviour
    {
        public InventoryMenuComponent _inventoryMenu;
        public List<ItemComponent> _items = new List<ItemComponent>();

        public ItemComponent GetDefaultItem() => (_items.Count > 0) ? _items[0] : null;

        public ItemComponent AddContents(ItemComponent item, PlayerComponent player)
        {
            if (!InventoryHelper.IsDuplicate(item, _items))
            {
                _items.Add(item);
                _inventoryMenu.AddItemToInventoryMenu(item);

                player.Await(item.IItemGetAnimation(), InputType.Character);

                return null;
            }
            return item;
        }

        public IEnumerator IAddContents(ItemComponent item, PlayerComponent player, Action<ItemComponent> callback)
        {
            if (!InventoryHelper.IsDuplicate(item, _items))
            {
                _items.Add(item);
                _inventoryMenu.AddItemToInventoryMenu(item);
                AudioManager.Instance.m_AudioSource.PlayOneShot(AudioManager.Instance.m_ItemGetSoundEffect, 0.1f);
                yield return player.IAwait(item.IItemGetAnimation(), InputType.Character);
                item.transform.SetParent(transform);
                callback(null);
            }
            else
            {
                callback(item);
            }
        }

        public void Open() => throw new System.NotImplementedException();
    }
}
