namespace Assets.Scripts.Components
{
    public class InventoryMenuComponent : MenuComponent
    {
        public void AddItemToInventoryMenu(ItemComponent item)
        {
            foreach (var option in _options)
            {
                var itemOption = (ItemMenuOptionComponent)option;

                if (itemOption.name == item.name)
                {
                    itemOption.SetItem(item);
                    return;
                }
            }
        }
    }
}
