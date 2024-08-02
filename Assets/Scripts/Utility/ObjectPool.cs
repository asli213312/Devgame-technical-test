using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private Queue<Transform> _objectsQueue = new Queue<Transform>();
    private int _poolSize;

    private Transform _prefab;

    public ObjectPool(GameObject parent, Transform prefab, int poolSize)
    {
        _prefab = prefab;

        for (int i = 0; i < poolSize; i++)
        {
            Transform newObj = InstantiateObj(Vector3.zero);
            newObj.parent = parent.transform;
            AddToPool(newObj);
        }
    }

    public void AddToPool(Transform obj)
    {
        obj.gameObject.SetActive(false);
        _objectsQueue.Enqueue(obj);
    }

    public Transform Get(Vector3 position)
    {
        if (_objectsQueue.Count == 0)
            return null;
        
        Transform newObj = _objectsQueue.Dequeue();
        newObj.transform.position = position;
        newObj.gameObject.SetActive(true);
        return newObj;
    }

    private Transform InstantiateObj(Vector3 position)
    {
        return UnityEngine.Object.Instantiate(_prefab, position, Quaternion.identity);
    }
}