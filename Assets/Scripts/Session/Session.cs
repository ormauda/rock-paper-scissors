using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Session : MonoBehaviour
{
    private void Awake()
    {
        SetUpSingletone();
    }

    private void SetUpSingletone()
    {
        int numberGameSessions = FindObjectsOfType<Session>().Length;
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
