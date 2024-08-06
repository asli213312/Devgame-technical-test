using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class AbstractPowerup : MonoBehaviour, IPowerup
{
    public event System.Action<AbstractPowerup> OnDestroy;
    public event System.Action<AbstractPowerup> OnTrigger;

    IPowerupConfig IPowerup.Data => Config;

    protected abstract IPowerupConfig Config { get; set; }

    protected Collider Collider { get; private set; }

    public void Accept(IPowerupVisitor powerupVisitor) => powerupVisitor.Visit(this);

    protected virtual void Start() 
    {
        Collider = GetComponent<Collider>();

        this.WaitForSeconds(Config.LifeTime, () =>
        {
            OnDestroy?.Invoke(this);
            Destroy(gameObject);
        });
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (IsTargetFound(other.gameObject) == false) return;

        OnTargetFound(gameObject);
    }

    protected virtual void OnTargetFound(GameObject gameObject) 
    {
        gameObject.SetActive(false);
        OnTrigger?.Invoke(this);

        Debug.Log("Picked powerup: " + name);
    }

    protected abstract bool IsTargetFound(GameObject gameObject);
}