using UnityEngine;

public class PistolBullet : AbstractBullet
{
    public PistolBullet(Transform transform, Transform startPosition, Vector3 direction, float speed, float radius) 
            : base(transform, startPosition, direction, speed, radius)
    {
        
    }
}