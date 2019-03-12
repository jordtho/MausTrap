namespace Assets.Scripts.Interfaces
{
    public interface IPlayerController : ICharacterController
    {
        void Interact(IInteractable target);
    }
}
