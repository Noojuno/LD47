using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI loopTimerText;
    public float timeRemaining = 30;

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }

        if (this.timeRemaining <= 0)
        {
            // LOOP
            // DISABLE INPUT
            // TELEPORT PLAYER TO START
            // RESET TIME REMAINING
        }

        TimeSpan time = TimeSpan.FromSeconds(this.timeRemaining);
        this.loopTimerText.SetText($"TIME REMAINING\n{time:mm\\:ss}");
    }
}
