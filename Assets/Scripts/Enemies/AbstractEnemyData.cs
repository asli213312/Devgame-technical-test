using UnityEngine;

public abstract class AbstractEnemyData : ScriptableObject
{
    [Header("Base")]
    [SerializeField] public float maxHealth;
    [SerializeField] public Transform prefab;

    [Header("Spawn options")]
    [SerializeField, Range(0, 1)] public float spawnChance;
    [SerializeField] public int maxCount;

    [Header("Unit options")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public int score;
    [Space(10)]

    [Header("Additional")]
    [SerializeField] private UnityEngine.SceneManagement.Scene blank2;
}