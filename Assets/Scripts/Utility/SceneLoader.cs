using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    #region Public Methods
    public void LoadCurrentScene() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(int sceneIndex) 
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadSceneAsync(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void UnloadSceneAsync(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    #endregion Public Methods
}