using UnityEngine;

[CreateAssetMenu(menuName = "Game/Zones/SlowZoneConfig")]
public class SlowZoneConfig : AbstractZoneConfig
{
    [SerializeField] private SlowZone prefab;

    public override AbstractZone Prefab { get => prefab; set => prefab = value as SlowZone; }
}