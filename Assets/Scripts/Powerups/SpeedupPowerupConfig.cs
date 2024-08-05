using UnityEngine;

[CreateAssetMenu(menuName = "Game/Powerups/Player/Speed Up")]
public class SpeedupPowerupConfig : TimedPowerupConfig
{
    [SerializeField] public PlayerSpeedup prefab;
    [SerializeField] public float speedMultiplier;

    protected override AbstractPowerup PowerupPrefab { get => prefab; set => prefab = value as PlayerSpeedup; }
}