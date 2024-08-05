using UnityEngine;

public abstract class AbstractPowerupFactory
{
    protected abstract AbstractPowerupConfig Config { get; set; }

    public AbstractPowerupFactory(AbstractPowerupConfig config) => Config = config;

    public IPowerup Create(Vector3 position) 
    {
        return null;
    }

    protected abstract IPowerup CreateEntity(Vector3 position);
}