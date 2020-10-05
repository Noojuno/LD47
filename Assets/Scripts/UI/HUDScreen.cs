using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDScreen : UIScreen<HUDScreen>
{
    public TextMeshProUGUI loopTimerText;

    void Update()
    {
        TimeSpan time = TimeSpan.FromSeconds(GameManager.Instance.timeRemaining);
        String color = GameManager.Instance.timeRemaining <= 5 ? "red" : "white";
        this.loopTimerText.SetText($"TIME REMAINING\n<color={color}>{time:mm\\:ss}</color>");
    }
}
