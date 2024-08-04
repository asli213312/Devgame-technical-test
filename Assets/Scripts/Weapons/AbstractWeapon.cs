using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public abstract class AbstractWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] protected Transform FirePoint;

    public event System.Action<Transform> OnCollideBullet;

    AbstractWeaponConfig IWeapon.Data => Config;

    float IDamageable.Damage => Config.damage;

    protected abstract AbstractWeaponConfig Config { get; set; }

    protected ObjectPool BulletsPool;

    protected List<AbstractBullet> ActiveBullets = new();

    protected virtual int BulletsPoolCount { get; } = 100;

    public void Handle() 
    {
        HandleBullets();
    }

    public virtual void Initialize() 
    {
        BulletsPool = new ObjectPool
        (
            new GameObject("BulletsPool"),
            Config.bulletPrefab, 
            BulletsPoolCount
        );
    }

    public virtual void Prepare() 
    {
        BulletsPool.Parent.SetActive(true);
    }

    public virtual void OnHideWeapon() 
    {
        BulletsPool.Parent.SetActive(false);
    }

    public abstract void Shoot();
    protected void HandleBullets() 
    {
        if (ActiveBullets.Count <= 0) return;

        Debug.Log("Active bullets: " + ActiveBullets.Count);

        for (int i = ActiveBullets.Count - 1; i >= 0; i--)
        {
            var bulletData = ActiveBullets[i];
            Debug.Log("Active bullet transform", bulletData.Transform.gameObject);

            bulletData.Transform.Translate(Vector3.right * Config.bulletSpeed * Time.deltaTime);
            bulletData.Update(Config.bulletSpeed);

            if (bulletData.DistanceTravelled > Config.maxDistance)
            {
                OnEndBullet(bulletData.Transform);
                continue;
            }

            if (bulletData.CheckCollision(Config.bulletRadius, out Collider[] hitColliders))
            {
                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.transform == bulletData.Transform) continue;
                    
                    OnCollideBullet?.Invoke(hitCollider.transform);
                    OnEndBullet(bulletData.Transform);
                    break;
                }
                continue;
            }
        }

        //Debug.Log($"Bullets in weapon {name}: {ActiveBullets.Count}");
    }

    protected virtual void OnEndBullet(Transform bullet) 
    {
        var bulletToRemove = ActiveBullets.FirstOrDefault(b => b.Transform == bullet);
        if (bulletToRemove != null)
        {
            ActiveBullets.Remove(bulletToRemove);
            BulletsPool.AddToPool(bullet);
        }
    }
}