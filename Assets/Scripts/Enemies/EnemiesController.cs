using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    [SerializeField] private EnemiesSpawner spawner;
    [SerializeField] private PlayerController playerController;

    public event System.Action<AbstractEnemyEntity> EnemyCollidedPlayer;

    public PlayerController PlayerController => playerController;

    private void Start() 
    {
        spawner.Initialize(this);

        spawner.OnEnemySpawned += playerController.WeaponController.CollisionHandler.AddTarget;
        playerController.CollisionHandler.OnCollide += CheckCollisionEnemies;

        EnemyCollidedPlayer += OnPlayerCollided;
    }

    private void OnDestroy() 
    {
        spawner.OnEnemySpawned -= playerController.WeaponController.CollisionHandler.AddTarget;
        playerController.CollisionHandler.OnCollide -= CheckCollisionEnemies;

        EnemyCollidedPlayer += OnPlayerCollided;
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
        if (playerController.Model.IsInvinsibility == false)
            playerController.gameObject.SetActive(false);
    }
}