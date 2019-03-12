namespace Assets.Scripts.Interfaces
{
    public interface IPlayerCharacter : ICharacter, IPlayerController, IAttacker
    {
        IInventory Inventory { get; set; }
    }
}
