public interface IWeaponable 
{
    AbstractWeapon CurrentWeapon { get; set; }
}

public interface IWeapon : IDamageable
{
    AbstractWeaponConfig Data { get; }
}

public interface IDamageable 
{
    float Damage { get; }
}