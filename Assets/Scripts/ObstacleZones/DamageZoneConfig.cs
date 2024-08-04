using UnityEngine;

[CreateAssetMenu(menuName = "Game/Zones/DamageZoneConfig")]
public class DamageZoneConfig : AbstractZoneConfig
{
    [SerializeField] private DamageZone prefab;

    protected override AbstractZone ZonePrefab { get => prefab; set => prefab = value as DamageZone; }
}