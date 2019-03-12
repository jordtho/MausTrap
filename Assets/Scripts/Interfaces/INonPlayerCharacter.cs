namespace Assets.Scripts.Interfaces
{
    public interface INonPlayerCharacter : ICharacter
    {
        IQuest AvailableQuest { get; set; }
    }
}
