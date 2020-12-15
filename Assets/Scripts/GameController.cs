using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject lostLabel;

    // Cached references
    ScreenLoader screenLoader;

    // Start is called before the first frame update
    void Start()
    {
        screenLoader = FindObjectOfType<ScreenLoader>();
        lostLabel.SetActive(false);
    }

    public void Lose()
    {
        lostLabel.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        lostLabel.SetActive(false);
        Time.timeScale = 1;
        screenLoader.RestartGame();
    }
}
