using UnityEngine;

public abstract class AbstractEnemyFactory 
{
    public AbstractEnemyData EnemyData { get; private set; }

    protected Transform Target { get; private set; }

    public AbstractEnemyFactory(AbstractEnemyData enemyData) 
    {
        EnemyData = enemyData;
    }

    public virtual void Initialize(Transform target) 
    {
        Target = target;
    }

    public AbstractEnemyEntity Create(Vector3 position) 
    {
        AbstractEnemyEntity entity = InstantiateEntity();
        Transform transform = UnityEngine.Object.Instantiate(EnemyData.prefab, position, Quaternion.identity);

        entity.Initialize(transform, Target);

        return entity;
    } 

    protected abstract AbstractEnemyEntity InstantiateEntity();
}

public class WeakEnemyFactory : AbstractEnemyFactory 
{
    public WeakEnemyFactory(WeakEnemyData enemyData) : base(enemyData) 
    {

    }

    protected override AbstractEnemyEntity InstantiateEntity() 
    {
        return new WeakEnemyEntity(EnemyData as WeakEnemyData, 
                new WeakEnemyModel
                (   EnemyData.maxHealth, 
                    EnemyData.moveSpeed
                ));
    }
}

public class FastEnemyFactory : AbstractEnemyFactory 
{
    public FastEnemyFactory(FastEnemyData enemyData) : base(enemyData) 
    {

    }

    protected override AbstractEnemyEntity InstantiateEntity() 
    {
        return new FastEnemyEntity(EnemyData as FastEnemyData, 
                new FastEnemyModel
                (   EnemyData.maxHealth, 
                    EnemyData.moveSpeed
                ));
    }
}

public class ArmoredEnemyFactory : AbstractEnemyFactory 
{
    public ArmoredEnemyFactory(ArmoredEnemyData enemyData) : base(enemyData) 
    {

    }

    protected override AbstractEnemyEntity InstantiateEntity() 
    {
        return new ArmoredEnemyEntity(EnemyData as ArmoredEnemyData, 
                new ArmoredEnemyModel
                (   EnemyData.maxHealth, 
                    EnemyData.moveSpeed
                ));
    }
}