using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI loopTimerText;
    public float loopLength = 10;
    public float timeRemaining = 5;

    private bool timerStarted = true;
    private PlayerController player;

    void Start()
    {
        GameObject gameObj = GameObject.FindGameObjectWithTag("Player");

        if (gameObj != null && gameObj.GetComponent<PlayerController>() != null)
        {
            this.player = gameObj.GetComponent<PlayerController>();
        }
    }

    void Update()
    {
        if (timeRemaining > 0 && this.timerStarted)
        {
            timeRemaining -= Time.deltaTime;
        }

        if (this.timeRemaining <= 0)
        {
            this.Loop();
        }

        TimeSpan time = TimeSpan.FromSeconds(this.timeRemaining);
        String color = this.timeRemaining <= 5 ? "red" : "white";
        this.loopTimerText.SetText($"TIME REMAINING\n<color={color}>{time:mm\\:ss}</color>");
    }

    void SetLoopLength(int length)
    {
        this.loopLength = length;
    }

    void SetTimeRemaining(int timeRemaning)
    {
        this.timeRemaining = timeRemaning;
    }

    void SetTimeRunning(bool running)
    {
        this.timerStarted = running;
    }

    void Loop()
    {
        this.timeRemaining = this.loopLength;
        this.player.transform.position = new Vector3(-3f, -2f);

        // LOOP
        // DISABLE INPUT
        // TELEPORT PLAYER TO START
        // RESET TIME REMAINING
    }
}
