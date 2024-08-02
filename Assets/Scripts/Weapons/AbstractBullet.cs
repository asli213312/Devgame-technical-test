using UnityEngine;

public abstract class AbstractBullet
{
    public Transform Transform;
    public readonly Transform StartPosition;
    public readonly Vector3 Direction;
    public float DistanceTravelled { get; private set; }

    public AbstractBullet(Transform transform, Transform startPosition, Vector3 direction) 
    {
        Transform = transform;
        StartPosition = startPosition;
        Direction = direction;
    }

    public void Update(float speed) => DistanceTravelled += speed * Time.deltaTime;

    public bool CheckCollision(float checkRadius, out Collider[] hitColliders)
    {
        hitColliders = Physics.OverlapSphere(Transform.position, checkRadius);
        return hitColliders.Length > 0;
    }
}