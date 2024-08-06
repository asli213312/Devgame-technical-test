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
        player.Model.OnDeath += () => Time.timeScale = 0;
        uiMediator.Initialize(player, SceneLoader);
    }

    private void Start() 
    {
        Time.timeScale = 1;
    }

    private void OnDestroy() 
    {
        player.Model.OnDeath -= () => Time.timeScale = 0;
    }

    private void InitializeServices() 
    {
        SceneLoader = new SceneLoader();
    }
}