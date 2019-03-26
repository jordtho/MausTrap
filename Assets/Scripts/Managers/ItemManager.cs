using Assets.Scripts.Components;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class ItemManager : MonoBehaviour
    {
        public bool _randomizeAllChests = false;

        public List<ChestComponent> _chests;
        public List<ItemComponent> _items;

        private void Awake()
        {
            RandomizeChests();
        }

        public void ClearChestItems(ChestComponent chest)
        {
            var items = chest.GetComponentsInChildren<ItemComponent>();
            foreach (var item in items) { Destroy(item.gameObject); }
        }

        public void RandomizeChests()
        {
            foreach (var chest in _chests)
            {
                if (chest._randomize || _randomizeAllChests)
                {
                    ClearChestItems(chest);
                    var item = GetRandomItem();
                    chest.Item = Instantiate(item, chest.transform);
                    chest.Item.name = item.name;
                    chest.Item.SpriteRenderer.enabled = false;
                }
            }
        }

        public ItemComponent GetRandomItem() => _items[Random.Range(0, _items.Count)];
    }
}
