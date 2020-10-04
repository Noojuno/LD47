using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public TextMeshProUGUI loopTimerText;
    public float loopLength = 10;
    public float timeRemaining = 5;
    public bool timerStarted = false;

    private PlayerController player;

    public PlayerController Player => this.player;

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

    public void SetLoopLength(int length)
    {
        this.loopLength = length;
    }

    public void SetTimeRemaining(int timeRemaning)
    {
        this.timeRemaining = timeRemaning;
    }

    public void SetTimeRunning(bool running)
    {
        this.timerStarted = running;
    }

    public void Loop()
    {
        this.timeRemaining = this.loopLength;
        this.player.transform.position = new Vector3(-3f, -2f);
        this.SetTimeRunning(false);

        // LOOP
        // DISABLE INPUT
        // TELEPORT PLAYER TO START
        // RESET TIME REMAINING
    }
}
