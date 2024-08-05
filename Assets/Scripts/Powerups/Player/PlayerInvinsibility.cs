using UnityEngine;

public class PlayerInvinsibility : PlayerPowerup, ITimedPowerup
{
    [SerializeField] private InvinsibilityPowerupConfig config;

    float ITimedPowerup.Duration => config.duration;

    protected override AbstractPowerupConfig Config { get => config; set => config = value as InvinsibilityPowerupConfig; }

    protected override void OnCollidePlayer(PlayerController playerController) 
    {
        playerController.Model.EnableInvinsibility();
    }

    void ITimedPowerup.OnTimeEnd() 
    {
        PlayerController.Model.DisableInvinsibility();
    }
}