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

    protected float ShootInterval { get; private set; }
    private float _nextShootTime;

    private void Update() 
    {
        HandleBullets();
    }

    public virtual void Handle() { }

    public virtual void Initialize(ObjectPool pool) 
    {
        BulletsPool = pool;

        ShootInterval = 60f / Config.fireRate;
    }

    public virtual void Prepare() 
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide() 
    {
        gameObject.SetActive(false);
    }

    public void Shoot() 
    {
        if (IsCanFire())
        {
            FireShoot();
            Debug.Log("Fired weapon");
        }
    }

    protected abstract void HandleShoot();

    protected void FireShoot() 
    {
        HandleShoot();
        _nextShootTime = Time.time + ShootInterval;
    }

    protected bool IsCanFire() => Time.time >= _nextShootTime;
    
    protected void HandleBullets() 
    {
        if (ActiveBullets.Count <= 0) return;

        //Debug.Log("Active bullets: " + ActiveBullets.Count);

        for (int i = ActiveBullets.Count - 1; i >= 0; i--)
        {
            var bulletData = ActiveBullets[i];
            //Debug.Log("Active bullet transform", bulletData.Transform.gameObject);

            bulletData.Transform.Translate(Vector3.right * bulletData.Speed * Time.deltaTime);
            bulletData.Update();

            if (bulletData.DistanceTravelled > Config.maxDistance)
            {
                OnEndBullet(bulletData.Transform);
                continue;
            }

            if (bulletData.CheckCollision(out Collider[] hitColliders))
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

    private void OnEndBullet(Transform bullet) 
    {
        var bulletToRemove = ActiveBullets.FirstOrDefault(b => b.Transform == bullet);
        if (bulletToRemove != null)
        {
            ActiveBullets.Remove(bulletToRemove);
            BulletsPool.AddToPool(bullet);
        }
        else 
        {
            Debug.LogError("Can't find bullet to remove!");
        }
    }
}