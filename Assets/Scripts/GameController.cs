using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] float secondsToWaitBeforeLost = 1f;
    [SerializeField] GameObject lostLabel;

    // Cached references
    ScreenLoader screenLoader;

    //Cached components
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        screenLoader = FindObjectOfType<ScreenLoader>();
        audioSource = GetComponent<AudioSource>();
        lostLabel.SetActive(false);
    }

    public void Lose()
    {
        StartCoroutine(handleLose());
    }

    public void RestartGame()
    {
        lostLabel.SetActive(false);
        Time.timeScale = 1;
        screenLoader.RestartGame();
    }

    private IEnumerator handleLose()
    {
        Time.timeScale = 0;
        audioSource.Play();
        yield return new WaitForSecondsRealtime(secondsToWaitBeforeLost);
        lostLabel.SetActive(true);
    }
}
