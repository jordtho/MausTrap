using Assets.Scripts.Interfaces;

namespace Assets.Scripts.DataModels
{
    class Chest : ItemContainer, IChest
    {
        public bool IsOpen { get; private set; }

        public Chest(IItem item = null)
        {
            PutItem(item);
        }

        public void Close() => IsOpen = false;

        public override void PutItem(IItem item)
        {
            base.PutItem(item);
            Close();
        }

        public override IItem TakeItem()
        {
            IsOpen = true;
            return base.TakeItem();
        }

        public void GetInteraction(IPlayerCharacter playerCharacter)
        {
            playerCharacter.Inventory.Add(TakeItem());
        }
    }
}
