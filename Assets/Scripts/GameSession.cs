using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{

    [SerializeField] int score = 0;
    [SerializeField] int life = 1;

    // Cached references
    ScreenLoader screenLoader;

    // Start is called before the first frame update
    void Start()
    {
        SetUpSingletone();
        screenLoader = FindObjectOfType<ScreenLoader>();
    }

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }
    public void ReduceLife()
    {
        life--;
        if (life <= 0)
        {
            screenLoader.LoadYouLose();
        }
    }

    internal object GetLife()
    {
        return life;
    }

    private void SetUpSingletone()
    {
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
