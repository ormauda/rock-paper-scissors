using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] SessionOptions sessionOptions;
    [SerializeField] int score = 0;
    [SerializeField] int life = 1;


    // Cached references
    GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        InitSession();
    }

    public void InitSession()
    {
        gameController = FindObjectOfType<GameController>();
        life = sessionOptions.GetInitialLife();
        score = sessionOptions.GetInitialScore();
    }

    public IEnumerator RestartGameSession()
    {
        Time.timeScale = 1;
        var asyncLoadLevelResult = SceneManager.LoadSceneAsync("Game Screen", LoadSceneMode.Single);
        while (!asyncLoadLevelResult.isDone)
        {
            yield return null;
        }
        InitSession();
    }

    public void RestartGame()
    {
        StartCoroutine(RestartGameSession());
    }

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }

    public void ReduceScore(int scoreToReduce)
    {
        score -= scoreToReduce;
    }

    public void ReduceLife()
    {
        life--;
        if (life <= 0)
        {
            gameController.Lose();
        }
    }

    internal object GetLife()
    {
        return life;
    }
}
