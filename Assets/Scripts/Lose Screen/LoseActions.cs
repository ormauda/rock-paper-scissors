using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseActions : MonoBehaviour
{
    // Cached references
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    public void RestartGame()
    {
        gameSession.RestartGame();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
