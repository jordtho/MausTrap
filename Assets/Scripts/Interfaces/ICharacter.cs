namespace Assets.Scripts.Interfaces
{
    public interface ICharacter : IDamageable, ICharacterController, I2DObjectView
    {
        string Name { get; set; }
        IItem EquippedItem { get; set; }

        void Equip(IItem item);
    }
}
