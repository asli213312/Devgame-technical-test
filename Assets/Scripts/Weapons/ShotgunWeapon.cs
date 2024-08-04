using System.Collections.Generic;
using UnityEngine;

public class ShotgunWeapon : AbstractWeapon
{
    [SerializeField] private ShotgunWeaponConfig config;

    protected override AbstractWeaponConfig Config { get => config; set => config = value as ShotgunWeaponConfig; }

    protected override int BulletsPoolCount => 200;

    public override void Shoot()
    {
        int counter = 0;

        int pelletCount = config.pelletCount;
        float spreadAngle = config.spreadAngle;

        float angleStep = spreadAngle / (pelletCount - 1);

        List<Transform> firedBullets = new();
        List<AbstractBullet> firedBulletsEntity = new();

        for (int i = 0; i < pelletCount; i++)
        {
            Transform bullet = BulletsPool.Get(FirePoint.position);
            if (bullet != null) firedBullets.Add(bullet);
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
                direction
            );

            if (!ActiveBullets.Contains(bulletEntity))
            {
                ActiveBullets.Add(bulletEntity);
                firedBulletsEntity.Add(bulletEntity);
                counter++;
            }
        }

        Debug.Log("Fired bullets count: " + firedBullets.Count);
        Debug.Log("Fired bulletsEntity count: " + firedBulletsEntity.Count);
    }
}