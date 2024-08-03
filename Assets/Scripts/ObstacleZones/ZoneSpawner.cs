using System.Collections.Generic;
using UnityEngine;

public class ZoneSpawner : MonoBehaviour
{
    [SerializeField] private float spawnTime;
    [SerializeField] private float minDistance;
    [SerializeField] private Collider mapBounds;
    [SerializeField] private PlayerController playerController;

    [SerializeField] private ZonesConfig config;

    private List<Vector3> _availablePositions = new List<Vector3>();
    private Dictionary<AbstractZone, int> _currentZones = new Dictionary<AbstractZone, int>();
    private float _timeSinceLastSpawn;

    private void Start()
    {
        GenerateFreePositions();
    }

    private void Update()
    {
        _timeSinceLastSpawn += Time.deltaTime;

        if (_timeSinceLastSpawn >= spawnTime)
        {
            SpawnZone();
            _timeSinceLastSpawn = 0;
        }
    }

    private void GenerateFreePositions()
    {
        Bounds bounds = mapBounds.bounds;
        Vector3 size = bounds.size;
        Vector3 min = bounds.min;
        Vector3 max = bounds.max;

        Vector3 adjustedMin = min + new Vector3(minDistance, 0, minDistance);
        Vector3 adjustedMax = max - new Vector3(minDistance, 0, minDistance);

        int gridSizeX = Mathf.CeilToInt((adjustedMax.x - adjustedMin.x) / minDistance);
        int gridSizeZ = Mathf.CeilToInt((adjustedMax.z - adjustedMin.z) / minDistance);

        _availablePositions.Clear();

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                Vector3 gridCenter = adjustedMin + new Vector3(x * minDistance + minDistance / 2, 0, z * minDistance + minDistance / 2);
                Vector3 randomOffset = new Vector3(
                    Random.Range(-minDistance / 2, minDistance / 2),
                    0,
                    Random.Range(-minDistance / 2, minDistance / 2)
                );
                Vector3 randomPosition = gridCenter + randomOffset;

                if (IsValidPosition(randomPosition))
                {
                    _availablePositions.Add(randomPosition);
                }
            }
        }
    }

    private bool IsValidPosition(Vector3 position)
    {
        Bounds bounds = mapBounds.bounds;
        if (!bounds.Contains(position))
        {
            return false;
        }

        foreach (Vector3 spawnPosition in _availablePositions)
        {
            if (Vector3.Distance(spawnPosition, position) < minDistance)
            {
                return false;
            }
        }

        return true;
    }

    private void SpawnZone()
    {
        if (_availablePositions.Count == 0) return;

        Vector3 spawnPosition = Vector3.zero;
        bool foundPosition = false;

        for (int i = 0; i < _availablePositions.Count; i++)
        {
            int randomIndex = Random.Range(0, _availablePositions.Count);
            spawnPosition = _availablePositions[randomIndex];

            if (Vector3.Distance(spawnPosition, playerController.transform.position) >= minDistance)
            {
                foundPosition = true;
                _availablePositions.RemoveAt(randomIndex);
                break;
            }
        }

        if (!foundPosition) return;

        AbstractZoneConfig randomZoneConfig = GetRandomZoneConfig();
        if (randomZoneConfig == null) return;

        AbstractZone zone = Instantiate(randomZoneConfig.Prefab, spawnPosition, Quaternion.identity);

        if (_currentZones.ContainsKey(randomZoneConfig.Prefab))
        {
            _currentZones[randomZoneConfig.Prefab]++;
        }
        else
        {
            _currentZones[randomZoneConfig.Prefab] = 1;
        }
    }

    private AbstractZoneConfig GetRandomZoneConfig()
    {
        List<AbstractZoneConfig> availableConfigs = new List<AbstractZoneConfig>();
        foreach (var zoneConfig in config.zones)
        {
            if (!IsMaxCountZonesType(zoneConfig))
            {
                availableConfigs.Add(zoneConfig);
            }
        }

        if (availableConfigs.Count == 0) return null;

        return availableConfigs[Random.Range(0, availableConfigs.Count)];
    }

    private bool IsMaxCountZonesType(AbstractZoneConfig zoneConfig)
    {
        if (_currentZones.ContainsKey(zoneConfig.Prefab))
        {
            return _currentZones[zoneConfig.Prefab] >= zoneConfig.maxCount;
        }
        return false;
    }
}