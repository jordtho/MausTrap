namespace Assets.Scripts.Interfaces
{
    public interface ISelectable
    {
        bool HasFocus { get; set; }

        void Select();
    }
}
