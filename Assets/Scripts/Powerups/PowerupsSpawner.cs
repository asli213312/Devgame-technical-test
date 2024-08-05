using System.Collections.Generic;
using UnityEngine;

public class PowerupsSpawner : MonoBehaviour
{
    [SerializeField] private PowerupsConfig config;
    [SerializeField] private Collider mapBounds;

    private event System.Action<float> SpawnTimerChanged;

    private List<AbstractPowerup> _powerups = new();

    private Camera _camera;

    private PowerupHandler _handler;

    private float TimeSinceLastSpawn 
    { 
        get => _timer;
        set 
        {
            _timer = value;
            SpawnTimerChanged?.Invoke(_timer);
        }
    }

    private float _timer;

    private void Awake() 
    {
        _camera = Camera.main;
        _handler = new PowerupHandler(this);

        SpawnTimerChanged += OnSpawnTimerChange;
    }

    private void OnDestroy() 
    {
        SpawnTimerChanged -= OnSpawnTimerChange;

        if (_powerups.Count <= 0) return;

        foreach (var powerup in _powerups)
        {
            powerup.OnTrigger -= (pw) => powerup.Accept(_handler);
        }
    }

    private void Update() 
    {
        TimeSinceLastSpawn += Time.deltaTime;
    }

    private void OnSpawnTimerChange(float time) 
    {
        foreach (var powerupConfig in config.powerups)
        {
            if (time <= powerupConfig.spawnInterval) continue;

            Spawn(powerupConfig);
        }

        if (time >= config.powerups[^1].spawnInterval)
            TimeSinceLastSpawn = 0;
    }

    private void Spawn(AbstractPowerupConfig powerupConfig) 
    {
        Vector3 randomPosition = GetSpawnPosition();

        var powerup = Instantiate(powerupConfig.Prefab, randomPosition, Quaternion.identity);

        OnPowerupSpawned(powerup);
    }

    private void OnPowerupSpawned(AbstractPowerup powerup) 
    {
        powerup.OnTrigger += (pw) => powerup.Accept(_handler);

        _powerups.Add(powerup);
    }

    private Vector3 GetSpawnPosition()
    {
        Bounds mapBounds = this.mapBounds.bounds;

        Vector3 cameraMin = _camera.ScreenToWorldPoint(new Vector3(0, 0, _camera.nearClipPlane));
        Vector3 cameraMax = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _camera.nearClipPlane));

        float minX = Mathf.Max(cameraMin.x, mapBounds.min.x);
        float maxX = Mathf.Min(cameraMax.x, mapBounds.max.x);
        float minZ = Mathf.Max(cameraMin.z, mapBounds.min.z);
        float maxZ = Mathf.Min(cameraMax.z, mapBounds.max.z);

        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);

        Vector3 spawnPosition = new Vector3(randomX, 0, randomZ);
        return spawnPosition;
    }
}