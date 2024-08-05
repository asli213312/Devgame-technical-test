using UnityEngine;

public class GrenadeLauncherWeapon : AbstractWeapon
{
    [SerializeField] private GrenadeLauncherConfig config;    

    protected override AbstractWeaponConfig Config { get => config; set => config = value as GrenadeLauncherConfig; }

    protected override void HandleShoot()
    {
        Vector3 targetPosition = GetTargetPosition();
        Transform grenadeTransform = BulletsPool.Get(FirePoint.position);
        grenadeTransform.rotation = FirePoint.rotation;

        // Расчёт направления и расстояния
        Vector3 direction = (targetPosition - FirePoint.position).normalized;
        float distance = Vector3.Distance(FirePoint.position, targetPosition);

        // Проверка на нулевое расстояние
        if (distance <= 0)
        {
            Debug.LogError("Target position is too close or identical to fire point.");
            return;
        }

        // Простой расчет угла для упрощенного броска
        float gravity = Physics.gravity.magnitude;
        float height = targetPosition.y - FirePoint.position.y;
        float horizontalDistance = Vector3.Distance(new Vector3(FirePoint.position.x, 0, FirePoint.position.z), new Vector3(targetPosition.x, 0, targetPosition.z));

        // Проверка на нулевой горизонтальный расстояние
        if (horizontalDistance <= 0)
        {
            Debug.LogError("Horizontal distance is too small.");
            return;
        }

        float angle = Mathf.Atan2(height, horizontalDistance);
        float initialSpeed = Mathf.Sqrt(gravity * horizontalDistance / Mathf.Sin(2 * angle));

        // Проверка на NaN
        if (float.IsNaN(initialSpeed) || float.IsNaN(direction.x) || float.IsNaN(direction.y))
        {
            Debug.LogError("Calculated initial speed or direction contains NaN values.");
            return;
        }

        Rigidbody rb = grenadeTransform.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = grenadeTransform.gameObject.AddComponent<Rigidbody>();
        }
        rb.useGravity = true;

        // Установка начальной скорости
        Vector3 velocity = new Vector3(direction.x * initialSpeed, direction.y * initialSpeed, direction.z * initialSpeed);
        rb.velocity = velocity;

        var grenade = new GrenadeBullet(
            grenadeTransform,
            FirePoint,
            direction,
            initialSpeed,
            config.bulletRadius,
            config.explosionRadius,
            config.explosionDamage,
            config.explosionDelay
        );

        ActiveBullets.Add(grenade);
    }

    private Vector3 GetTargetPosition()
    {
        // Реализация получения позиции прицеливания
        // Например, с помощью Raycast для точного получения позиции на уровне
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, FirePoint.position); // Предположим, что граната летит по плоскости XZ
        if (plane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }
        return FirePoint.position; // Возвращаем начальную позицию, если не удалось определить
    }
}