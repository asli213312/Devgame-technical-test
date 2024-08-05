using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public event System.Action<GameObject> OnCollide;

    private void OnCollisionEnter(Collision other)
    {
        OnCollide?.Invoke(other.gameObject);
    }
}