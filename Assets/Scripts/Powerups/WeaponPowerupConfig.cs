using UnityEngine;

[CreateAssetMenu(menuName = "Game/Powerups/Player/Random weapon")]
public class WeaponPowerupConfig : TimedPowerupConfig
{
    [SerializeField] public WeaponPowerup prefab;
    [SerializeField] public AbstractWeaponConfig[] weapons;

    protected override AbstractPowerup PowerupPrefab { get => prefab; set => prefab = value as WeaponPowerup; }
}