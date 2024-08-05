public interface IPowerup
{
    void Accept(IPowerupVisitor visitor);
}

public interface ITimedPowerup : IPowerup
{
    float Duration { get; }
    void OnTimeEnd();
}

public interface ISinglePowerup : IPowerup 
{

}

public interface IPowerupVisitor 
{
    void Visit(IPowerup powerup);
}

public interface ITimedPowerupVisitor : IPowerupVisitor
{
    void Visit(ITimedPowerup timedPowerup);
}

public interface ISinglePowerupVisitor : IPowerupVisitor
{
    void Visit(ISinglePowerup singlePowerup);
}