namespace Assets.Scripts.Interfaces
{
    public interface IQuest
    {
        IItem ObjectiveItem { get; set; }
        IQuest FollowUpQuest { get; set; }
    }
}
