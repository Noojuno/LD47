using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDScreen : UIScreen<HUDScreen>
{
    public TextMeshProUGUI loopTimerText;
    public TextMeshProUGUI loopImminentText;

    void Update()
    {
        this.loopTimerText.gameObject.SetActive(GameManager.Instance.showTimer);
        this.loopImminentText.gameObject.SetActive(GameManager.Instance.timeRemaining < 5f);

        TimeSpan time = TimeSpan.FromSeconds(GameManager.Instance.timeRemaining);
        String color = GameManager.Instance.timeRemaining <= 5 ? "red" : "white";
        this.loopTimerText.SetText($"TIME REMAINING\n<color={color}>{time:mm\\:ss}</color>");
    }

    public override void OnEscapePressed()
    {
        PauseScreen.Open();
    }
}
