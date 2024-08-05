public interface IWeaponable 
{
    AbstractWeapon CurrentWeapon { get; set; }
}

public interface IWeapon : IDamageable
{
    AbstractWeaponConfig Data { get; }

    void Initialize(ObjectPool bulletPool);
    void Shoot();
    void Prepare();
    void Handle();
    void Hide();
}

public interface IDamageable
{
    float Damage { get; }
}