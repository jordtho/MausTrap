namespace Assets.Scripts.Interfaces
{
    public interface IChest : IItemContainer, IInteractable
    {
        bool IsOpen { get; }

        void Close();
    }
}
