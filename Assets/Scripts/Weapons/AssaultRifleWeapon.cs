using UnityEngine;

public class AssaultRifleWeapon : AbstractWeapon
{
    [SerializeField] private AssaultRifleConfig config;

    protected override AbstractWeaponConfig Config { get => config; set => config = value as AssaultRifleConfig; }

    private bool _isShooting;
    private float _shootInterval;
    private float _nextShootTime;

    public override void Initialize()
    {
        base.Initialize();

        _shootInterval = 60f / config.fireRate;
    }

    public override void Handle() 
    {
        base.Handle();

        if (Input.GetMouseButton(0))
        {
            if (Time.time >= _nextShootTime)
            {
                Shoot();
                _nextShootTime = Time.time + _shootInterval;
            }
        }
        else 
        {
            _isShooting = false;
        }
    }

    public override void Shoot() 
    {
        Transform bullet = BulletsPool.Get(FirePoint.position);
        bullet.transform.rotation = FirePoint.rotation;

        Vector3 direction = FirePoint.forward;
        bullet.transform.forward = direction;

        bullet.transform.position = FirePoint.position;

        ActiveBullets.Add(new PistolBullet
        (
            bullet.transform,
            bullet.transform,
            direction,
            config.bulletSpeed,
            config.bulletRadius
        ));
    }
}