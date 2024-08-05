using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerController : MonoBehaviour, IZoneInteractable
{
    [SerializeField] private Transform playerObj;
    [SerializeField] private Mover mover;
    [SerializeField] private PlayerCollisionHandler collisionHandler;
    [SerializeField, SerializeReference] private AbstractWeapon[] weapons;

    [Header("Options")]
    [SerializeField] private PlayerOptions options;

    public event System.Action OnCompleteRotation;

    public Mover Mover => mover;
    public PlayerCollisionHandler CollisionHandler => collisionHandler;
    public WeaponController WeaponController { get; private set; }
    public PlayerModel Model { get; private set; }

    private Quaternion _targetRotation;
    private float _rotationSpeed;
    private bool _hasCompletedRotation;

    private void Awake() 
    {
        Model = new PlayerModel();

        InitializeModules();

        _rotationSpeed = options.rotationSpeed;

        foreach (var weapon in weapons)
        {
            OnCompleteRotation += WeaponController.Shoot;   
        }
    }

    private void OnDestroy() 
    {
        foreach (var weapon in weapons)
        {
            OnCompleteRotation -= WeaponController.Shoot;   
        }
    }

    private void Update() 
    {
        HandlePlayerRotation();

        mover.Handle();
        WeaponController.Handle();
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

    private void InitializeModules() 
    {
        WeaponController = new WeaponController(weapons);
        mover.Initialize();
    }
}