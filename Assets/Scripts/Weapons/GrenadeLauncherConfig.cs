using UnityEngine;

[CreateAssetMenu(menuName = "Game/Weapons/GrenadeLauncher Config")]
public class GrenadeLauncherConfig : AbstractWeaponConfig
{
    [SerializeField] public float explosionRadius;
    [SerializeField] public float explosionDamage;
    [SerializeField] public float explosionDelay;
}