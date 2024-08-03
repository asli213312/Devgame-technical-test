using UnityEngine;

[CreateAssetMenu(menuName = "Game/Zones/DamageZoneConfig")]
public class DamageZoneConfig : AbstractZoneConfig
{
    [SerializeField] private DamageZone prefab;

    public override AbstractZone Prefab { get => prefab; set => prefab = value as DamageZone; }
}