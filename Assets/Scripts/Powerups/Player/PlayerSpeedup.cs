using UnityEngine;

public class PlayerSpeedup : PlayerPowerup, ITimedPowerup
{
    [SerializeField] private SpeedupPowerupConfig config;

    protected override IPowerupConfig Config { get => config; set => config = value as SpeedupPowerupConfig; }

    float ITimedPowerup.Duration => config.duration;

    private float _initialSpeed;

    protected override void OnCollidePlayer(PlayerController playerController) 
    {
        _initialSpeed = playerController.Mover.Speed;
        playerController.Mover.Speed *= config.speedMultiplier;
    }

    void ITimedPowerup.OnTimeEnd() 
    {
        PlayerController.Mover.Speed = _initialSpeed;
    }
}