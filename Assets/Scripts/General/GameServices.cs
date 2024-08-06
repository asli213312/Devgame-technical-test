using UnityEngine;

public class GameServices : MonoBehaviour
{
    [SerializeField] private UIMediator uiMediator;
    [SerializeField] private PlayerController player;

    public UIMediator UI => uiMediator;
    public PlayerController Player => player;
    public SceneLoader SceneLoader { get; private set; }

    private void Awake() 
    {
        InitializeServices();

        player.Initialize();
        uiMediator.Initialize(player, SceneLoader);
    }

    private void InitializeServices() 
    {
        SceneLoader = new SceneLoader();
    }
}