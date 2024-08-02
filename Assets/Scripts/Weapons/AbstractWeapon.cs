using UnityEngine;
using System.Collections.Generic;

public abstract class AbstractWeapon : MonoBehaviour
{
    [SerializeField] protected Transform FirePoint;
    protected abstract AbstractWeaponConfig Config { get; set; }

    protected ObjectPool BulletsPool;

    protected List<AbstractBullet> ActiveBullets = new();

    private const int BULLETS_POOL_COUNT = 100;

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
            BULLETS_POOL_COUNT
        );
    }

    public abstract void Shoot();
    protected abstract void HandleBullets();
}