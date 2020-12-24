using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenLoader : MonoBehaviour
{
    // Cached references
    GameSession gameSession;

    public void Start()
    {
        gameSession = FindObjectOfType<GameSession>();

    }

    public IEnumerable RestartGame()
    {
        Time.timeScale = 1;
        var asyncLoadLevelResult = SceneManager.LoadSceneAsync("Game Screen", LoadSceneMode.Single);
        while (!asyncLoadLevelResult.isDone)
        {
            yield return null;
        }
        gameSession.InitSession();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game Screen");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Welcome Screen");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits Screen");
    }

    public void LoadHowToPlay()
    {
        SceneManager.LoadScene("How To Play Screen");
    }
}
