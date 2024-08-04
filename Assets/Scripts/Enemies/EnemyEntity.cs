using UnityEngine;

public abstract class AbstractEnemyEntity 
{
    public event System.Action<AbstractEnemyEntity> OnDeath;

    public AbstractEnemyData Data => EnemyData;
    public AbstractEnemyModel Model => EnemyModel;
    public Transform Transform { get; private set; }

    protected abstract AbstractEnemyData EnemyData { get; set; }
    protected abstract AbstractEnemyModel EnemyModel { get; set; }

    protected Transform Target { get; private set; }

    public AbstractEnemyEntity(AbstractEnemyData data, AbstractEnemyModel model) 
    {
        EnemyData = data;
        EnemyModel = model;

        EnemyModel.OnHealthChanged += OnHealthChanged;
    }

    ~AbstractEnemyEntity() 
    {
        EnemyModel.OnHealthChanged -= OnHealthChanged;
    }

    public virtual void Initialize(Transform transform, Transform target) 
    {
        Transform = transform;
        Target = target;
    }

    public void Update() 
    {
        if (Target == null) return;

        Handle();
    }

    protected virtual void Handle() 
    {
        Transform.position = Vector3.MoveTowards(Transform.position, Target.position, Model.Speed * Time.deltaTime);
    }

    private void OnHealthChanged(float health)
    {
        if (health <= 0)
        {
            HandleDeath();
        }
    }

    private void HandleDeath() 
    {
        Transform.gameObject.SetActive(false);
        OnDeath?.Invoke(this);
    }
}

public class WeakEnemyEntity : AbstractEnemyEntity 
{
    protected override AbstractEnemyData EnemyData { get => _data; set => _data = value as WeakEnemyData; }
    protected override AbstractEnemyModel EnemyModel { get => _model; set => _model = value as WeakEnemyModel; }

    private WeakEnemyData _data;
    private WeakEnemyModel _model;

    public WeakEnemyEntity(WeakEnemyData data, WeakEnemyModel model) : base(data, model)
    {
        
    }

    protected override void Handle() 
    {
        base.Handle();
    }
} 

public class FastEnemyEntity : AbstractEnemyEntity 
{
    protected override AbstractEnemyData EnemyData { get => _data; set => _data = value as FastEnemyData; }
    protected override AbstractEnemyModel EnemyModel { get => _model; set => _model = value as FastEnemyModel; }
    private FastEnemyData _data;
    private FastEnemyModel _model;

    public FastEnemyEntity(FastEnemyData data, FastEnemyModel model) : base(data, model)
    {
        
    }

    protected override void Handle() 
    {
        base.Handle();
    }
}

public class ArmoredEnemyEntity : AbstractEnemyEntity 
{
    protected override AbstractEnemyData EnemyData { get => _data; set => _data = value as ArmoredEnemyData; }
    protected override AbstractEnemyModel EnemyModel { get => _model; set => _model = value as ArmoredEnemyModel; }
    private ArmoredEnemyData _data;
    private ArmoredEnemyModel _model;

    public ArmoredEnemyEntity(ArmoredEnemyData data, ArmoredEnemyModel model) : base(data, model)
    {
        
    }

    protected override void Handle() 
    {
        base.Handle();
    }
} 