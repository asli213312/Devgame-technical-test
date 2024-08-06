using UnityEngine;

public class PlayerModel
{
    public event System.Action OnDeath;

    public bool IsInvinsibility { get; private set; }

    public void EnableInvinsibility() => IsInvinsibility = true;
    public void DisableInvinsibility() => IsInvinsibility = false;

    public void HandleDeath() 
    {
        OnDeath?.Invoke();
    }
}