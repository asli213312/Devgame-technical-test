using System.Collections.Generic;
using UnityEngine;

public class WeaponPowerup : PlayerPowerup
{
    [SerializeField] private WeaponPowerupConfig config;

    protected override IPowerupConfig Config { get => config; set => config = value as WeaponPowerupConfig; }

    private AbstractWeaponConfig _initialWeapon;

    protected override void OnCollidePlayer(PlayerController playerController) 
    {
        playerController.WeaponController.ChooseWeapon(GetRandomUniqueWeapon());
    }

    private AbstractWeaponConfig GetRandomUniqueWeapon() 
    {
        List<AbstractWeaponConfig> uniqueWeapons = new();
        var currentWeapon = PlayerController.WeaponController.CurrentWeaponData;

        _initialWeapon = currentWeapon;

        foreach (var weapon in config.weapons)
        {
            if (weapon != currentWeapon) uniqueWeapons.Add(weapon);
        }

        return uniqueWeapons[Random.Range(0, uniqueWeapons.Count)];
    }
}