using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerController : MonoBehaviour, IZoneInteractable
{
    [SerializeField] private Transform playerObj;
    [SerializeField] private Mover mover;
    [SerializeField, SerializeReference] private AbstractWeapon weapon;

    [Header("Options")]
    [SerializeField] private PlayerOptions options;

    public event System.Action OnCompleteRotation;

    private Quaternion _targetRotation;

    private float _health;
    private float _rotationSpeed;
    private bool _hasCompletedRotation;

    private void Start() 
    {
        mover.Initialize();
        weapon.Initialize();

        _rotationSpeed = options.rotationSpeed;
        _health = options.health;

        OnCompleteRotation += weapon.Shoot;
    }

    private void OnDestroy() 
    {
        OnCompleteRotation -= weapon.Shoot;
    }

    private void Update() 
    {
        HandlePlayerRotation();

        mover.Handle();
        weapon.Handle();
    }

    private void HandlePlayerRotation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseScreenPosition = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
            Plane groundPlane = new Plane(Vector3.up, playerObj.position);
            float distanceToGroundPlane;

            if (groundPlane.Raycast(ray, out distanceToGroundPlane))
            {
                Vector3 mouseWorldPosition = ray.GetPoint(distanceToGroundPlane);

                Vector3 direction = (mouseWorldPosition - playerObj.position).normalized;

                _targetRotation = Quaternion.LookRotation(direction);

                _hasCompletedRotation = false;
            }
        }


        playerObj.rotation = Quaternion.Slerp(playerObj.rotation, _targetRotation, Time.deltaTime * _rotationSpeed);

        if (!_hasCompletedRotation && Quaternion.Angle(playerObj.rotation, _targetRotation) < 0.1f)
        {
            OnCompleteRotation?.Invoke();
            _hasCompletedRotation = true;
        }
    }
}