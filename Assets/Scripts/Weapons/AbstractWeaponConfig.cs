using UnityEngine;

public abstract class AbstractWeaponConfig : ScriptableObject
{
    [Header("Bullet")]
    [SerializeField] public float bulletSpeed;
    [SerializeField] public float bulletRadius;
    [SerializeField] public Transform bulletPrefab;    

    [Header("Weapon")]
    [SerializeField] public float maxDistance;
    [SerializeField] public float damage;
}