using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Components
{
    public class EquippedItemHUDComponent : MonoBehaviour
    {
        public Image _equippedItemImageComponent;
        public Text _equippedItemTextComponent;

        public void UpdateEquippedItemComponent(Item item)
        {
            _equippedItemTextComponent.text = item?.m_Quantity.ToString();
            _equippedItemImageComponent.sprite = item?.SpriteRenderer.sprite;
            _equippedItemTextComponent.enabled = item != null;
            _equippedItemImageComponent.enabled = item != null;
        }
    }
}
