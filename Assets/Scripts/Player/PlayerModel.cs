using UnityEngine;

public class PlayerModel
{
    public bool IsInvinsibility { get; private set; }

    public void EnableInvinsibility() => IsInvinsibility = true;
    public void DisableInvinsibility() => IsInvinsibility = false;
}