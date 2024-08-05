using UnityEngine;

public class PowerupHandler : ITimedPowerupVisitor, ISinglePowerupVisitor
{
    private readonly MonoBehaviour _mono;

    public PowerupHandler(MonoBehaviour mono) 
    {
        _mono = mono;
    }

    public void Visit(IPowerup powerup) 
    {
        switch(powerup) 
        {
            case ITimedPowerup timedPowerup: Visit(timedPowerup); break;
            case ISinglePowerup singlePowerup: Visit(singlePowerup); break;
        }
    }

    public void Visit(ITimedPowerup timedPowerup) 
    {
        Debug.Log("Visited timed powerup");
        _mono.WaitForSeconds(timedPowerup.Duration, () =>
        {
            timedPowerup.OnTimeEnd();
            Debug.Log($"Timed powerup with name {timedPowerup.GetType().Name} is ended");
        });
    }

    public void Visit(ISinglePowerup powerup) 
    {
        Debug.Log("Visited single powerup");
    }
}