using System.Collections.Generic;

namespace Assets.Scripts.Components.Menus
{
    public class InventoryMenuComponent : MenuComponent
    {
        public List<ItemMenuOptionComponent> _itemOptions;

        public void AddItemToMenu(ItemComponent item)
        {
            foreach (var option in _itemOptions)
            {
                if (option.name == item.name)
                {
                    option.SpriteRenderer.enabled = true;
                    option.Item = item;
                    return;
                }
            }
        }
    }
}
