using UnityEngine;

public abstract class AbstractBullet
{
    public readonly Transform Transform;
    public readonly Transform StartPosition;
    public readonly Vector3 Direction;
    public readonly float Speed;
    public readonly float Radius;
    public float DistanceTravelled { get; private set; }

    public AbstractBullet(Transform transform, Transform startPosition, Vector3 direction, float speed, float radius) 
    {
        Transform = transform;
        StartPosition = startPosition;
        Direction = direction;
        Speed = speed;
        Radius = radius;
    }

    public virtual void Update() => DistanceTravelled += Speed * Time.deltaTime;

    public virtual bool CheckCollision(out Collider[] hitColliders)
    {
        hitColliders = Physics.OverlapSphere(Transform.position, Radius);
        return hitColliders.Length > 0;
    }
}