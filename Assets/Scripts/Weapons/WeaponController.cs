using System.Collections.Generic;
using UnityEngine;

public class WeaponController 
{
    public AbstractWeaponConfig CurrentWeaponData { get; private set; }
    public WeaponCollisionHandler CollisionHandler { get; private set; }

    private IWeapon _currentWeapon;
    private IWeapon[] _weapons;
    private List<ObjectPool> _bulletsPools = new();

    private int _initialWeaponIndex;

    public WeaponController(IWeapon[] weapons) 
    {
        _weapons = weapons;

        CollisionHandler = new WeaponCollisionHandler(weapons as AbstractWeapon[]);

        Initialize();

        _initialWeaponIndex = ChooseRandomWeapon();
    }

    public void Shoot() 
    {
        _currentWeapon.Shoot();
    }

    public void Handle() 
    {
        _currentWeapon.Handle();
    }

    public void ChooseInitialWeapon() 
    {
        EquipWeaponByIndex(_initialWeaponIndex);
    }

    public int ChooseWeapon(AbstractWeaponConfig nextWeaponConfig) 
    {
        int selectedIndex = 0;

        for (int i = 0; i < _weapons.Length; i++)
        {
            var weapon = _weapons[i];

            if (weapon.Data != nextWeaponConfig) continue;

            EquipWeaponByIndex(i);
            selectedIndex = i;

            break;
        }

        return selectedIndex;
    }

    private int ChooseRandomWeapon() 
    {
        return EquipWeaponByIndex(Random.Range(0, _weapons.Length));
    }

    private int EquipWeaponByIndex(int index) 
    {
        if (_currentWeapon != null) _currentWeapon.Hide();

        _currentWeapon = _weapons[index];
        _currentWeapon.Prepare();

        RefreshPoolCurrentWeapon();

        CurrentWeaponData = _currentWeapon.Data;

        Debug.Log("Equipped other weapon: " + _currentWeapon.GetType().Name);

        return index;
    }

    private void RefreshPoolCurrentWeapon() 
    {
        foreach (var pool in _bulletsPools)
        {
            pool.Hide();
        }
    }

    private void Initialize() 
    {
        InitializePools();
        InitializeWeapons();
    }

    private void InitializeWeapons() 
    {
        foreach (var w in _weapons)
        {
            foreach (var pool in _bulletsPools)
            {
                if (w.Data.bulletPrefab != pool.Prefab) continue;

                w.Initialize(pool);
                break;
            }
        }
    }

    private void InitializePools() 
    {
        List<Transform> bulletTypes = new();

        foreach (var w in _weapons)
        {
            if (bulletTypes.Contains(w.Data.bulletPrefab)) continue;

            bulletTypes.Add(w.Data.bulletPrefab);
        }

        Debug.Log("Added bullets types: " + bulletTypes.Count);

        for (int i = 0; i < bulletTypes.Count; i++)
        {
            var bulletTransform = bulletTypes[i];
            _bulletsPools.Add(new ObjectPool
            (
                new GameObject("BulletsPool"),
                bulletTransform, 
                300
            ));
        }
    }
}