namespace Assets.Scripts.Interfaces
{
    public interface IConsumable : IItem
    {
        int Quantity { get; set; }
    }
}
