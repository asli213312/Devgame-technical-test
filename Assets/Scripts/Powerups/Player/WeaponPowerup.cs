using UnityEngine;

public class WeaponPowerup : PlayerPowerup, ITimedPowerup
{
    [SerializeField] private WeaponPowerupConfig config;

    float ITimedPowerup.Duration => config.duration;

    protected override IPowerupConfig Config { get => config; set => config = value as WeaponPowerupConfig; }

    protected override void OnCollidePlayer(PlayerController playerController) 
    {
        
    }

    void ITimedPowerup.OnTimeEnd() 
    {
        
    }
}