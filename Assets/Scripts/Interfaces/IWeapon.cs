namespace Assets.Scripts.Interfaces
{
    public interface IWeapon : IItem
    {
        IDamage Damage { get; set; }
        int Knockback { get; set; }
    }
}
