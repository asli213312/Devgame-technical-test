using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    public readonly GameObject Parent;
    public readonly Transform Prefab;

    private Queue<Transform> _objectsQueue = new Queue<Transform>();

    public ObjectPool(GameObject parent, Transform prefab, int poolSize)
    {
        Prefab = prefab;
        Parent = parent;

        for (int i = 0; i < poolSize; i++)
        {
            Transform newObj = InstantiateObj(Vector3.zero);
            newObj.parent = parent.transform;
            AddToPool(newObj);
        }
    }

    public void Hide() 
    {
        Transform[] objectsArray = _objectsQueue.ToArray();

        for (int i = objectsArray.Length - 1; i >= 0; i--)
        {
            AddToPool(objectsArray[i]);
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
        {
            Debug.LogError("Pool queue is empty to get object!");
            return null;
        }
        
        Transform newObj = _objectsQueue.Dequeue();
        newObj.transform.position = position;
        newObj.gameObject.SetActive(true);
        return newObj;
    }

    private Transform InstantiateObj(Vector3 position)
    {
        return UnityEngine.Object.Instantiate(Prefab, position, Quaternion.identity);
    }
}