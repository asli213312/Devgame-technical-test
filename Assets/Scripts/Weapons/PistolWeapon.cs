using System.Collections;
using System.Linq;
using UnityEngine;

public class PistolWeapon : AbstractWeapon
{
    [SerializeField, SerializeReference] private AbstractWeaponConfig config;

    protected override AbstractWeaponConfig Config { get => config; set => config = value; }

    public override void Shoot() 
    {
        Transform bullet = BulletsPool.Get(FirePoint.position);

        bullet.transform.rotation = FirePoint.rotation;

        ActiveBullets.Add(new PistolBullet
            (
                bullet.transform,
                bullet.transform,
                FirePoint.forward
            ));
    }

    protected override void HandleBullets() 
    {
        if (ActiveBullets.Count <= 0) return;

        for (int i = 0; i < ActiveBullets.Count; i++)
        {
            var bulletData = ActiveBullets[i];

            bulletData.Transform.Translate(Vector3.right * config.bulletSpeed * Time.deltaTime);
            bulletData.Update(config.bulletSpeed);

            if (bulletData.DistanceTravelled > config.maxDistance)
            {
                OnEndBullet(bulletData.Transform);
                i--;
                continue;
            }

            if (bulletData.CheckCollision(config.bulletRadius, out Collider[] hitColliders))
            {
                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.transform == bulletData.Transform) continue;
                    
                    OnEndBullet(bulletData.Transform);
                    i--;
                    break;
                }
                continue;
            }
        }
    }

    private void OnEndBullet(Transform bullet) 
    {
        BulletsPool.AddToPool(bullet);

        var bulletToRemove = ActiveBullets.FirstOrDefault(b => b.Transform == bullet);
        if (bulletToRemove != null)
        {
            ActiveBullets.Remove(bulletToRemove);
        }
    }
}