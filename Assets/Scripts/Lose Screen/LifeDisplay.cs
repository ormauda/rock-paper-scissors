using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LifeDisplay : MonoBehaviour
{
    // Cached references
    TextMeshProUGUI lifeText;
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        lifeText = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        lifeText.SetText(gameSession.GetLife().ToString());
    }
}
