using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Components
{
    public class EquippedItemHUDComponent : MonoBehaviour
    {
        public Image _equippedItemImageComponent;
        public Text _equippedItemTextComponent;

        public void UpdateEquippedItemComponent(ItemComponent item)
        {
            _equippedItemTextComponent.text = item?._quantity.ToString();
            _equippedItemImageComponent.sprite = item?.SpriteRenderer.sprite;
            _equippedItemTextComponent.enabled = item != null;
            _equippedItemImageComponent.enabled = item != null;
        }
    }
}
