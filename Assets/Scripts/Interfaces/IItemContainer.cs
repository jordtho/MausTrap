namespace Assets.Scripts.Interfaces
{
    public interface IItemContainer
    {
        IItem Contents { get; }

        void PutItem(IItem item);
        IItem TakeItem();
    }
}
