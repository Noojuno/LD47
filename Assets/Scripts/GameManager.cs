﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public float loopLength = 10;
    public float timeRemaining = 10;
    public bool runTimer = false;

    public Portal loopPortal;

    private PlayerController _player;

    public PlayerController Player => this._player;

    void Start()
    {
        GameObject gameObj = GameObject.FindGameObjectWithTag("Player");

        if (gameObj != null && gameObj.GetComponent<PlayerController>() != null)
        {
            this._player = gameObj.GetComponent<PlayerController>();
        }

        this._player.transform.position = this.loopPortal.transform.position;
    }

    void Update()
    {
        if (timeRemaining > 0 && this.runTimer)
        {
            timeRemaining -= Time.deltaTime;
        }

        if (this.timeRemaining <= 0)
        {
            this.Loop();
        }
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
        this.runTimer = running;
    }

    public void SetPaused(bool paused)
    {
        this.SetTimeRunning(!paused);
        this.Player.movementEnabled = !paused;
    }

    public void Loop()
    {
        //this._player.movementEnabled = false;
        this.timeRemaining = this.loopLength;
        this._player.transform.position = this.loopPortal.transform.position;
        this._player.facing = Vector2.down;
        this.SetTimeRunning(false);

        // LOOP
        // DISABLE INPUT
        // TELEPORT PLAYER TO START
        // RESET TIME REMAINING
    }
}
