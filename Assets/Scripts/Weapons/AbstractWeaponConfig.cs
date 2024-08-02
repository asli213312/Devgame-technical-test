using UnityEngine;

public abstract class AbstractWeaponConfig : ScriptableObject
{
    [SerializeField] public float bulletSpeed;
    [SerializeField] public float bulletRadius;
    [SerializeField] public float maxDistance;
    [SerializeField] public Transform bulletPrefab;    
}