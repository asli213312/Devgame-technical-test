using System.Collections.Generic;
using UnityEngine;

public class WeaponCollisionHandler 
{
    public event System.Action<Transform, AbstractWeapon> OnCollideBullet;

    private AbstractWeapon[] _weapons;

    private List<AbstractEnemyEntity> _enemies = new();
    private List<IWeaponable> _weaponables = new();

    private IWeapon _currentWeapon;

    public WeaponCollisionHandler(AbstractWeapon[] weapons) 
    {
        _weapons = weapons;   

        foreach (var w in weapons)
        {
            w.OnCollideBullet += (target) => OnCollide(target, w);
        }
    }

    ~WeaponCollisionHandler()
    {
        foreach (var w in _weapons)
        {
            w.OnCollideBullet -= (target) => OnCollide(target, w);
        }
    }

    public void AddWeaponable(IWeaponable weaponable) => _weaponables.Add(weaponable);
    public void RemoveWeaponable(IWeaponable weaponable) 
    {
        if (_weaponables.Contains(weaponable))
            _weaponables.Remove(weaponable);
    }

    public void AddTarget(AbstractEnemyEntity target) 
    {
        if (_enemies.Contains(target) == false)
            _enemies.Add(target);
    }

    public void RemoveTarget(AbstractEnemyEntity target) 
    {
        if (_enemies.Contains(target))
            _enemies.Remove(target);
    }

    private void OnCollide(Transform target, AbstractWeapon weapon) 
    {
        OnCollideBullet?.Invoke(target, weapon);

        if (_enemies.Count <= 0) return;

        _currentWeapon = weapon;

        for (int i = 0; i < _enemies.Count; i++)
        {
            AbstractEnemyEntity enemyEntity = _enemies[i];

            if (enemyEntity.Transform != target) continue;

            OnEnemyCollide(enemyEntity);
        }
    }

    private void OnEnemyCollide(AbstractEnemyEntity enemyEntity) 
    {
        enemyEntity.Model.TakeDamage(_currentWeapon.Damage);
    }
}