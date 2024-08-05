using System.Collections;
using System.Linq;
using UnityEngine;

public class PistolWeapon : AbstractWeapon
{
    [SerializeField, SerializeReference] private AbstractWeaponConfig config;

    protected override AbstractWeaponConfig Config { get => config; set => config = value; }

    protected override void HandleShoot() 
    {
        Transform bullet = BulletsPool.Get(FirePoint.position);

        bullet.transform.rotation = FirePoint.rotation;

        ActiveBullets.Add(new PistolBullet
            (
                bullet.transform,
                bullet.transform,
                FirePoint.forward,
                config.bulletSpeed,
                config.bulletRadius
            ));
    }
}