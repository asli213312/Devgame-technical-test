using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    private static readonly List<IUpdatable> _entities = new();

    private void Awake() 
    {
        if (_entities.Count > 0)
            _entities.Clear();
    }

    private void Update() 
    {
        for (int i = _entities.Count - 1; i > 0; i--)
        {
            _entities[i]?.Update();
        }
    }

    private void OnDestroy() 
    {
        _entities.Clear();
    }

    public static void AddEntity(IUpdatable entity) 
    {
        _entities.Add(entity);
    }

    public static void RemoveEntity(IUpdatable entity) 
    {
        _entities.Remove(entity);
    }
}