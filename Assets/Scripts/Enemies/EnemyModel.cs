using UnityEngine;

public abstract class AbstractEnemyModel 
{
    public event System.Action<float> OnHealthChanged;
    public float Speed { get; protected set; }

    private float Health
    {
        get => _health;
        set
        {
            _health = value;
            OnHealthChanged?.Invoke(_health);
        }
    }

    private float _health;

    public AbstractEnemyModel(float health, float speed) 
    {
        Health = health;
        Speed = speed;
    }

    public void TakeDamage(float damage) 
    {
        Health -= damage;
    }
}

public class WeakEnemyModel : AbstractEnemyModel 
{
    public WeakEnemyModel(float health, float speed) : base(health, speed) 
    {

    }
}

public class FastEnemyModel : AbstractEnemyModel 
{
    public FastEnemyModel(float health, float speed) : base(health, speed) 
    {

    }
}

public class ArmoredEnemyModel : AbstractEnemyModel 
{
    public ArmoredEnemyModel(float health, float speed) : base(health, speed) 
    {

    }
}