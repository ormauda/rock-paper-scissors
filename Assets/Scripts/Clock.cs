﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    float timeSinceStart;

    // Cached references
    TextMeshProUGUI clockText;

    // Start is called before the first frame update
    void Start()
    {
        clockText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceStart += Time.deltaTime;
        clockText.text = timeSinceStart.ToString("F1");
    }

    public float GetTimeSinceStart()
    {
        return timeSinceStart;
    }
}
