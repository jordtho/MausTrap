using UnityEngine.UI;

namespace Assets.Scripts.Components
{
    public class ItemMenuOptionComponent : MenuOptionComponent
    {
        public ItemComponent _item;
        public Image _image;

        public ItemComponent Item { get => _item; set => _item = value; }
        public Image Image { get => _image; set => _image = value; }

        public void SetItem(ItemComponent item)
        {
            Item = item;
            Image = GetComponent<Image>();
            Image.enabled = true;
        }

        public override void OnSelect() => throw new System.NotImplementedException();
    }
}