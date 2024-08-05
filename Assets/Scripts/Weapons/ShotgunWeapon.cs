using System.Collections.Generic;
using UnityEngine;

public class ShotgunWeapon : AbstractWeapon
{
    [SerializeField] private ShotgunWeaponConfig config;

    protected override AbstractWeaponConfig Config { get => config; set => config = value as ShotgunWeaponConfig; }

    protected override void HandleShoot()
    {
        int pelletCount = config.pelletCount;
        float spreadAngle = config.spreadAngle;

        float angleStep = spreadAngle / (pelletCount - 1);

        if (BulletsPool == null) 
        {
            Debug.LogError("Bullets pool is empty");
            return;
        }

        for (int i = 0; i < pelletCount; i++)
        {
            Transform bullet = BulletsPool.Get(FirePoint.position);

            if (bullet == null) 
            {
                Debug.LogError("Bullet is empty to spawn");
                return;
            }
            bullet.transform.rotation = FirePoint.rotation;

            float angle = -spreadAngle / 2 + i * angleStep;

            Vector3 direction = FirePoint.forward;
            direction = Quaternion.Euler(0, angle, 0) * direction;

            bullet.transform.position = FirePoint.position;
            bullet.transform.forward = direction.normalized;

            var bulletEntity = new PistolBullet
            (
                bullet.transform,
                bullet.transform,
                direction,
                config.bulletSpeed,
                config.bulletRadius
            );

            if (!ActiveBullets.Contains(bulletEntity))
            {
                ActiveBullets.Add(bulletEntity);
            }
        }
    }
}