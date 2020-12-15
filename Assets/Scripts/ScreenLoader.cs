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
}
