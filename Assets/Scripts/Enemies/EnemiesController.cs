using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    [SerializeField] private EnemiesSpawner spawner;
    [SerializeField] private GameServices services;

    public event System.Action<AbstractEnemyEntity> EnemyCollidedPlayer;

    public PlayerController PlayerController => services.Player;

    private void Start() 
    {
        spawner.Initialize(this);

        spawner.OnEnemySpawned += OnEnemySpawned;
        spawner.OnEnemySpawned += services.Player.WeaponController.CollisionHandler.AddTarget;
        services.Player.CollisionHandler.OnCollide += CheckCollisionEnemies;

        EnemyCollidedPlayer += OnPlayerCollided;
    }

    private void OnDestroy() 
    {
        spawner.OnEnemySpawned -= OnEnemySpawned;
        spawner.OnEnemySpawned -= services.Player.WeaponController.CollisionHandler.AddTarget;
        services.Player.CollisionHandler.OnCollide -= CheckCollisionEnemies;

        EnemyCollidedPlayer -= OnPlayerCollided;
    }

    private void OnEnemySpawned(AbstractEnemyEntity enemyEntity) 
    {
        enemyEntity.OnDeath += OnEnemyDeath;
    }

    private void CheckCollisionEnemies(GameObject gameObject) 
    {
        for (int i = 0; i < spawner.Enemies.Count; i++)
        {
            AbstractEnemyEntity enemyEntity = spawner.Enemies[i];

            if (enemyEntity.Transform != gameObject.transform) continue;

            EnemyCollidedPlayer?.Invoke(enemyEntity);
        }
    }

    private void OnPlayerCollided(AbstractEnemyEntity enemyEntity) 
    {
        if (services.Player.Model.IsInvinsibility == false)
            services.Player.Model.HandleDeath();
    }

    private void OnEnemyDeath(AbstractEnemyEntity enemyEntity) 
    {
        enemyEntity.OnDeath -= OnEnemyDeath;

        services.UI.ChangeScore(enemyEntity.Data.score);
    }
}