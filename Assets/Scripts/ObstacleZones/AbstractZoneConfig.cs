using UnityEngine;

public abstract class AbstractZoneConfig : ScriptableObject
{
    [Header("Base")]
    [SerializeField] public int maxCount;

    [Header("Additional")]
    [SerializeField] private UnityEngine.SceneManagement.Scene blank;
    public abstract AbstractZone Prefab { get; set; }
}