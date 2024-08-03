using UnityEngine;

[CreateAssetMenu(menuName = "Game/Zones/Config")]
public class ZonesConfig : ScriptableObject
{
    [SerializeField] public AbstractZoneConfig[] zones;
}