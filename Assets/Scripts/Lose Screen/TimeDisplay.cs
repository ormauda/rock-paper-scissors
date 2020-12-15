using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeDisplay : MonoBehaviour
{
    // Cached references
    TextMeshProUGUI timeText;
    Clock clock;

    // Start is called before the first frame update
    void Start()
    {
        timeText = GetComponent<TextMeshProUGUI>();
        clock = FindObjectOfType<Clock>();
    }

    // Update is called once per frame
    void Update()
    {
        timeText.SetText(clock.GetTimeSinceStart().ToString("F1"));
    }
}
