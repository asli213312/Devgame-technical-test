using UnityEngine;

[CreateAssetMenu(menuName = "Game/Powerups/Player/Invinsibility")]
public class InvinsibilityPowerupConfig : TimedPowerupConfig
{
    [SerializeField] public PlayerInvinsibility prefab;

    protected override IPowerup PowerupPrefab { get => prefab; set => prefab = value as PlayerInvinsibility; }
}