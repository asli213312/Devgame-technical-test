using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    [Header("Options")]
    [SerializeField] private float speed;

    public Vector3 Offset => offset;
    public Camera Camera => _camera;

    public float Speed => speed;

    private Dictionary<string, bool> _restrictions = new();

    private Camera _camera;

    private void Awake() 
    {
        _camera = GetComponent<Camera>();
    }

    public void LateHandle() 
    {
        if (target == null) return;
        if (IsHasRestrictions()) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, speed * Time.deltaTime);
        transform.position = smoothedPosition;
    }

    public void AddRestriction(string key, bool restriction)
    {
        if (!_restrictions.ContainsKey(key))
        {
            _restrictions.Add(key, restriction);
        }
        else
        {
            Debug.LogWarning($"Restriction with key '{key}' already exists.");
        }
    }

    public void RemoveRestriction(string key)
    {
        if (_restrictions.ContainsKey(key))
        {
            _restrictions.Remove(key);
        }
        else
        {
            Debug.LogWarning($"Restriction with key '{key}' does not exist.");
        }
    }

    private bool IsHasRestrictions() 
    {
        int counter = 0;

        foreach (var restriction in _restrictions)
        {
            if (restriction.Value) counter++;
        }

        return counter > 0;
    }
}