using UnityEngine;

public class SlowZone : AbstractZone
{
    [SerializeField] private SlowZoneConfig config;
    [SerializeField, Range(0, 1)] private float slowPercentage;
    private float _originalSpeed;
    private float _slowedSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IMovable movable))
        {
            _originalSpeed = movable.Speed;
            _slowedSpeed = _originalSpeed * (1 - slowPercentage);
            movable.Speed = _slowedSpeed;

            InvokeTrigger();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IMovable movable))
        {
            movable.Speed = _originalSpeed;
        }
    }
}