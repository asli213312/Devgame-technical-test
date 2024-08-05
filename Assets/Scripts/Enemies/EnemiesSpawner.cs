using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private EnemiesSpawnerConfig config;
    [SerializeField] private Collider mapCollider;
    [SerializeField] private float offScreenDistance;

    [Header("Runtime options")]
    [SerializeField] private float spawnInterval;
    [SerializeField] private float spawnRateDecrease;
    [SerializeField] private float minSpawnInterval;
    [SerializeField] private float decreaseInterval;

    public event System.Action<AbstractEnemyEntity> OnEnemySpawned;

    public List<AbstractEnemyEntity> Enemies { get; private set; } = new();

    private List<AbstractEnemyFactory> _factories = new();

    private EnemiesController _enemiesController;

    private Bounds _mapBounds;

    private float _timeSinceLastSpawn;
    private float _timeSinceLastDecrease;
    private float _spawnInterval;

    private void Update() 
    {
        HandleSpawnFactories();
        HandleSpawnTime();
        HandleEnemies();
    }

    public void Initialize(EnemiesController enemiesController) 
    {
        _enemiesController = enemiesController;

        InitializeFactories();

        _spawnInterval = spawnInterval;
        _mapBounds = mapCollider.bounds;
    }

    private void HandleSpawnFactories() 
    {
        if (_timeSinceLastSpawn < _spawnInterval) return;

        foreach (var f in _factories)
        {
            if (Random.Range(0, 1f) > f.EnemyData.spawnChance) continue;

            AbstractEnemyEntity enemyEntity = f.Create(GetSpawnPosition());

            Enemies.Add(enemyEntity);

            OnEnemySpawned?.Invoke(enemyEntity);

            _timeSinceLastSpawn = 0;
        }
    }

    private void HandleEnemies() 
    {
        if (Enemies.Count <= 0) return;

        for (int i = 0; i < Enemies.Count; i++)
        {
            AbstractEnemyEntity enemyEntity = Enemies[i];

            enemyEntity.Update();
        }
    }

    private void HandleSpawnTime() 
    {
        _timeSinceLastSpawn += Time.deltaTime;
        _timeSinceLastDecrease += Time.deltaTime;

        if (_timeSinceLastDecrease >= decreaseInterval)
        {
            _timeSinceLastDecrease = 0f;

            if (_spawnInterval > minSpawnInterval)
            {
                _spawnInterval -= spawnRateDecrease;
                _spawnInterval = Mathf.Max(_spawnInterval, minSpawnInterval);
            }
        }
    }

    private Vector3 GetSpawnPosition()
    {
        Bounds mapBounds = _mapBounds;
        Vector3 spawnPosition = Vector3.zero;

        int side = Random.Range(0, 4);

        switch (side)
        {
            case 0: // Up
                spawnPosition = new Vector3(
                    Random.Range(mapBounds.min.x - offScreenDistance, mapBounds.max.x + offScreenDistance),
                    0,
                    mapBounds.max.z + Random.Range(0, offScreenDistance)
                );
                break;
            case 1: // Left
                spawnPosition = new Vector3(
                    mapBounds.min.x - Random.Range(0, offScreenDistance),
                    0,
                    Random.Range(mapBounds.min.z - offScreenDistance, mapBounds.max.z + offScreenDistance)
                );
                break;
            case 2: // Right
                spawnPosition = new Vector3(
                    mapBounds.max.x + Random.Range(0, offScreenDistance),
                    0,
                    Random.Range(mapBounds.min.z - offScreenDistance, mapBounds.max.z + offScreenDistance)
                );
                break;
            case 3: // Down
                spawnPosition = new Vector3(
                    Random.Range(mapBounds.min.x - offScreenDistance, mapBounds.max.x + offScreenDistance),
                    0,
                    mapBounds.min.z - Random.Range(0, offScreenDistance)
                );
                break;
        }

        return spawnPosition;
    }

    private void InitializeFactories() 
    {
        foreach (var enemyData in config.enemies)
        {
            CreateFactory(enemyData);
        }
    }

    private void CreateFactory(AbstractEnemyData enemyData) 
    {
        AbstractEnemyFactory selectedFactory = null;

        switch (enemyData) 
        {
            case WeakEnemyData:
                    WeakEnemyFactory weakFactory = new WeakEnemyFactory(enemyData as WeakEnemyData);
                    weakFactory.Initialize(_enemiesController.PlayerController.transform);
                    selectedFactory = weakFactory; 
                    break;
            case FastEnemyData: 
                    FastEnemyFactory fastFactory = new FastEnemyFactory(enemyData as FastEnemyData);
                    fastFactory.Initialize(_enemiesController.PlayerController.transform);
                    selectedFactory = fastFactory; 
                    break;
            case ArmoredEnemyData:
                    ArmoredEnemyFactory armoredFactory = new ArmoredEnemyFactory(enemyData as ArmoredEnemyData);
                    armoredFactory.Initialize(_enemiesController.PlayerController.transform);
                    selectedFactory = armoredFactory;
                    break;
        }

        _factories.Add(selectedFactory);
    }
}