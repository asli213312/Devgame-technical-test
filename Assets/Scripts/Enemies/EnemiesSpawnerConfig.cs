using UnityEngine;

[CreateAssetMenu(menuName = "Game/Enemies/SpawnerConfig")]
public class EnemiesSpawnerConfig : ScriptableObject
{
    [SerializeField] public AbstractEnemyData[] enemies;
}