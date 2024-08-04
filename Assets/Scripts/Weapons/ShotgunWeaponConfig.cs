using UnityEngine;

[CreateAssetMenu(menuName = "Game/Weapons/Shotgun Config")]
public class ShotgunWeaponConfig : AbstractWeaponConfig
{
    [SerializeField] public int pelletCount;
    [SerializeField] public float spreadAngle;
}