using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIMediator : MonoBehaviour
{
    [SerializeField] private ScoreCounter scoreCounter;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private RectTransform gameOverPanel;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;

    private PlayerController _playerController;
    private SceneLoader _sceneLoader;

    public void Initialize(PlayerController playerController, SceneLoader sceneLoader) 
    {
        _playerController = playerController;
        _playerController.Model.OnDeath += OnPlayerDeath;

        _sceneLoader = sceneLoader;

        restartButton.onClick.AddListener(_sceneLoader.LoadCurrentScene);
        mainMenuButton.onClick.AddListener(() => _sceneLoader.LoadScene(0));
    }

    public void ChangeScore(int value) 
    {
        scoreCounter.ChangeValue(value);
    }

    private void OnDestroy() 
    {
        _playerController.Model.OnDeath -= OnPlayerDeath;
        restartButton.onClick.RemoveListener(_sceneLoader.LoadCurrentScene);
        mainMenuButton.onClick.RemoveListener(() => _sceneLoader.LoadScene(0));
    }

    private void OnPlayerDeath() 
    {
        scoreCounter.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(true);

        UpdatePlayerHighScore();
    }

    private void UpdatePlayerHighScore() 
    {
        int lastHighScore = PlayerPrefs.GetInt(Constants.Player.PLAYER_SCORE, 0);
        int currentScore = scoreCounter.GetValue();
        string scoreText;

        if (currentScore > lastHighScore) 
        {
            PlayerPrefs.SetInt(Constants.Player.PLAYER_SCORE, currentScore);
            scoreText = "New record! Score: " + currentScore;
        } 
        else 
        {
            scoreText = "Score: " + currentScore;
        }

        gameOverText.text = scoreText;
    }
}