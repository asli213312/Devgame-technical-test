using UnityEngine;

public class GrenadeBullet : AbstractBullet
{
    private Vector3 _targetPosition;
    private float _explosionRadius;
    private float _explosionDamage;
    private float _explosionDelay;
    private float _timer;
    private bool _hasExploded;

    public GrenadeBullet(
        Transform transform, 
        Transform startPosition, 
        Vector3 direction, 
        float speed,
        float radius, 
        float explosionRadius, 
        float explosionDamage, 
        float explosionDelay
    ) 
        : base(transform, startPosition, direction, speed, radius)
    {
        _explosionRadius = explosionRadius;
        _explosionDamage = explosionDamage;
        _explosionDelay = explosionDelay;
    }

    public override void Update() 
    {
        base.Update();

        _timer += Time.deltaTime;

        if (_timer >= _explosionDelay)
        {
            Explode();
        }
    }

    private void Explode()
    {
        if (_hasExploded) return;

        _hasExploded = true;

        Object.Destroy(Transform.gameObject);
    }

    public override bool CheckCollision(out Collider[] hitColliders)
    {
        hitColliders = Physics.OverlapSphere(Transform.position, _explosionRadius);
        return hitColliders.Length > 0;
    }
}