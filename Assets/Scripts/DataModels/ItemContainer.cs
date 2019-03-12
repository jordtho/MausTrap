using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.DataModels
{
    public class ItemContainer : MonoBehaviour, IItemContainer
    {
        public IItem Contents { get; private set; }

        public virtual void PutItem(IItem item) => Contents = item;

        public virtual IItem TakeItem()
        {
            var item = Contents;
            Contents = null;
            return item;
        }
    }
}
