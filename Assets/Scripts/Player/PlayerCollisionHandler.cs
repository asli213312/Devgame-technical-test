using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(Collider))]
public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private Collider bounds;

    public event System.Action<GameObject> OnCollide;

    private Vector3 _playerSize;

    private void Start()
    {
        if (TryGetComponent(out Collider playerCollider))
        {
            _playerSize = playerCollider.bounds.size;
        }
    }

    private void Update()
    {
        RestrictPlayerToBounds();
    }

    private void RestrictPlayerToBounds()
    {
        if (bounds == null)
        {
            Debug.LogWarning("Bounds collider is not assigned.");
            return;
        }

        Bounds boundsArea = bounds.bounds;
        Vector3 playerPosition = transform.position;
        Vector3 playerHalfSize = _playerSize / 2;

        playerPosition.x = Mathf.Clamp(playerPosition.x, boundsArea.min.x + playerHalfSize.x, boundsArea.max.x - playerHalfSize.x);
        playerPosition.z = Mathf.Clamp(playerPosition.z, boundsArea.min.z + playerHalfSize.z, boundsArea.max.z - playerHalfSize.z);

        transform.position = playerPosition;
    }

    private void OnCollisionEnter(Collision collision) 
    {
        OnCollide?.Invoke(collision.gameObject);
    }
}