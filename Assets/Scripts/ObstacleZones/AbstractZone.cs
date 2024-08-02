using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class AbstractZone : MonoBehaviour
{
    [SerializeField] private float chanceSpawn;

    public event System.Action OnTrigger;

    public float ChanceSpawn => chanceSpawn;

    protected void InvokeTrigger() => OnTrigger?.Invoke();
}