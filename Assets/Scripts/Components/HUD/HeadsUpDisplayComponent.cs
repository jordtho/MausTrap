using UnityEngine;

namespace Assets.Scripts.Components
{
    public class HeadsUpDisplayComponent : MonoBehaviour
    {
        public EquippedItemHUDComponent _equippedItem;
        public MoneyHUDComponent _money;
        public HealthBarHUDComponent _healthBar;

        public void UpdateEquippedItem(Item item) => _equippedItem.UpdateEquippedItemComponent(item);

        public void UpdateHealth(int value) => _healthBar.SetCurrentHealth(value);

        public void UpdateMoney(int value) => _money.UpdateMoneyComponent(value);
    }
}
