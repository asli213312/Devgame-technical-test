using UnityEngine;

[CreateAssetMenu(menuName = "Game/Powerups/Config")]
public class PowerupsConfig : ScriptableObject
{
    [SerializeField, SerializeReference] public AbstractPowerupConfig[] powerups;
}