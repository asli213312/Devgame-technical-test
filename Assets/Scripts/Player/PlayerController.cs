using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerController : MonoBehaviour, IZoneInteractable, IWeaponable
{
    [SerializeField] private Transform playerObj;
    [SerializeField] private Mover mover;
    [SerializeField, SerializeReference] private AbstractWeapon[] weapons;

    [Header("Options")]
    [SerializeField] private PlayerOptions options;

    public event System.Action OnCompleteRotation;

    public AbstractWeapon CurrentWeapon { get; set; }
    public WeaponCollisionHandler WeaponCollisionHandler { get; private set; }

    private Quaternion _targetRotation;

    private float _health;
    private float _rotationSpeed;
    private bool _hasCompletedRotation;

    private void Start() 
    {
        CurrentWeapon = weapons[Random.Range(0, weapons.Length)];

        InitializeModules();

        _rotationSpeed = options.rotationSpeed;
        _health = options.health;

        foreach (var weapon in weapons)
        {
            OnCompleteRotation += weapon.Shoot;   
        }
    }

    private void OnDestroy() 
    {
        foreach (var weapon in weapons)
        {
            OnCompleteRotation -= weapon.Shoot;   
        }
    }

    private void Update() 
    {
        HandlePlayerRotation();

        mover.Handle();
        CurrentWeapon.Handle();
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
        WeaponCollisionHandler = new WeaponCollisionHandler(weapons);
        mover.Initialize();

        InitializeWeapons();

        void InitializeWeapons() 
        {
            foreach (var w in weapons)
            {
                w.Initialize();

                if (w != CurrentWeapon) 
                {
                    w.gameObject.SetActive(false);
                    w.OnHideWeapon();
                }
            }
        }
    }
}