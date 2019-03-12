using Assets.Scripts.Interfaces;

namespace Assets.Scripts.DataModels
{
    public class NonPlayerCharacter : Character, INonPlayerCharacter, INonPlayerController
    {
        public IQuest AvailableQuest { get; set; }
    }
}
