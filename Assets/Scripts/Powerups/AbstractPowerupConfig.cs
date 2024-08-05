using UnityEngine;

public abstract class AbstractPowerupConfig : ScriptableObject, IPowerupConfig
{
    [Header("Base")]
    [SerializeField] public int maxCount;
    [SerializeField] public float lifeTime;
    [SerializeField] public float spawnInterval;

    [Header("Additional")]
    [SerializeField] private UnityEngine.SceneManagement.Scene blank;
    
    IPowerup IPowerupConfig.Prefab => PowerupPrefab;
    float IPowerupConfig.LifeTime => lifeTime;

    protected abstract IPowerup PowerupPrefab { get; set; }
}