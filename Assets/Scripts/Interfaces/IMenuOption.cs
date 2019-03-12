namespace Assets.Scripts.Interfaces
{
    public interface IMenuNode : ISelectable
    {
        IMenu SubMenu { get; set; }
    }
}
