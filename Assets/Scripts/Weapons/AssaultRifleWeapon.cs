using UnityEngine;

public class AssaultRifleWeapon : AbstractWeapon
{
    [SerializeField] private AssaultRifleConfig config;

    protected override AbstractWeaponConfig Config { get => config; set => config = value as AssaultRifleConfig; }

    public override void Handle() 
    {
        base.Handle();

        if (Input.GetMouseButton(0) && IsCanFire())
        {
            FireShoot();
        }
    }

    protected override void HandleShoot() 
    {
        Transform bullet = BulletsPool.Get(FirePoint.position);
        bullet.transform.rotation = FirePoint.rotation;

        Vector3 direction = FirePoint.forward;
        bullet.transform.forward = direction;

        bullet.transform.position = FirePoint.position;

        var bulletEntity = new PistolBullet
        (
            bullet.transform,
            bullet.transform,
            direction,
            config.bulletSpeed,
            config.bulletRadius
        );

        ActiveBullets.Add(bulletEntity);
    }
}