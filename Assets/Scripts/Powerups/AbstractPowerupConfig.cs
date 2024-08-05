using UnityEngine;

public abstract class AbstractPowerupConfig : ScriptableObject
{
    [Header("Base")]
    [SerializeField] public int maxCount;
    [SerializeField] public float lifeTime;
    [SerializeField] public float spawnInterval;

    [Header("Additional")]
    [SerializeField] private UnityEngine.SceneManagement.Scene blank;

    public AbstractPowerup Prefab => PowerupPrefab;

    protected abstract AbstractPowerup PowerupPrefab { get; set; }
}