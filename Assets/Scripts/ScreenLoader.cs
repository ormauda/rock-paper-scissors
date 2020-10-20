using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenLoader : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("Game Screen");
    }

    public void LoadYouLose()
    {
        SceneManager.LoadScene("Lose Screen");
    }
}
