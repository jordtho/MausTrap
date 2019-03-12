namespace Assets.Scripts.Interfaces
{
    public interface IAttacker : ICharacter
    {
        IWeapon Weapon { get; set; }

        void Attack();
    }
}
