using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIMenuMediator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private Button startGameButton;

    private void Awake() 
    {
        ShowHighScore();

        startGameButton.onClick.AddListener(() => SceneManager.LoadScene(1));
    }

    private void OnDestroy() 
    {
        startGameButton.onClick.RemoveListener(() => SceneManager.LoadScene(1));
    }

    private void ShowHighScore() 
    {
        if (PlayerPrefs.HasKey(Constants.Player.PLAYER_SCORE)) 
        {
            var highScore = PlayerPrefs.GetInt(Constants.Player.PLAYER_SCORE);
            highScoreText.text = "Highscore: " + highScore;
        }
    }
}