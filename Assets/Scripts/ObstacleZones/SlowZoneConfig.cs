using UnityEngine;

[CreateAssetMenu(menuName = "Game/Zones/SlowZoneConfig")]
public class SlowZoneConfig : AbstractZoneConfig
{
    [SerializeField] private SlowZone prefab;

    protected override AbstractZone ZonePrefab { get => prefab; set => prefab = value as SlowZone; }
}