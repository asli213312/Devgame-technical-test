using System.Collections;
using System.Linq;
using UnityEngine;

public class PistolWeapon : AbstractWeapon
{
    [SerializeField, SerializeReference] private AbstractWeaponConfig config;

    protected override AbstractWeaponConfig Config { get => config; set => config = value; }

    protected override void HandleShoot() 
    {
        Transform bulletTransform = BulletsPool.Get(FirePoint.position);

        bulletTransform.rotation = FirePoint.rotation;

        var bulletEntity = new PistolBullet
        (
            bulletTransform,
            bulletTransform,
            FirePoint.forward,
            config.bulletSpeed,
            config.bulletRadius
        );

        ActiveBullets.Add(bulletEntity);
    }
}