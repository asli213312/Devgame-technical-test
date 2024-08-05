using UnityEngine;

public abstract class PlayerPowerup : AbstractPowerup
{
    protected PlayerController PlayerController { get; private set; }

    protected override bool IsTargetFound(GameObject gameObject) 
    {
        if (gameObject.TryGetComponent(out PlayerController playerController) == false) return false;

        PlayerController = playerController;

        OnCollidePlayer(playerController);
        return true;
    }

    protected abstract void OnCollidePlayer(PlayerController playerController);
}