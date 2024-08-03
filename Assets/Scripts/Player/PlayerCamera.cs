using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private CameraFollower cameraFollower;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Collider mapBoundsCollider;

    private Bounds _mapBounds;

    private void Start() 
    {
        _mapBounds = mapBoundsCollider.bounds;        
    }

    private void LateUpdate() 
    {
        cameraFollower.LateHandle();
        RestrictCameraWithinBounds();
    }

    private void RestrictCameraWithinBounds()
    {
        Vector3 targetPosition = cameraFollower.transform.position + cameraFollower.Offset;

        Bounds bounds = _mapBounds;
        Vector3 min = bounds.min;
        Vector3 max = bounds.max;

        Vector3 playerPosition = playerController.transform.position;

        float cameraHalfWidth = cameraFollower.Camera.orthographicSize * cameraFollower.Camera.aspect;
        float cameraHalfHeight = cameraFollower.Camera.orthographicSize;

        float clampedX = Mathf.Clamp(targetPosition.x, min.x + cameraHalfWidth, max.x - cameraHalfWidth);
        float clampedY = Mathf.Clamp(targetPosition.y, min.y + cameraHalfHeight, max.y - cameraHalfHeight);
        float clampedZ = Mathf.Clamp(targetPosition.z, min.z + cameraHalfHeight, max.z - cameraHalfHeight);

        cameraFollower.transform.position = new Vector3(clampedX, clampedY, clampedZ);
    }
}