using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class Keypad : Interactable
{
    public SpriteRenderer buttonSpriteRenderer;
    public Color keypadColor;
    public bool isPermanent = true;
    public bool isSolved = false;
    public string correctCode = "1234";
    public UnityEvent OnCorrectCodeEvent;

    public override void Start()
    {
        base.Start();

        if (this.OnCorrectCodeEvent == null)
        {
            this.OnCorrectCodeEvent = new UnityEvent();
        }

        this.OnCorrectCodeEvent.AddListener(this.OnCorrectCode);

        this.buttonSpriteRenderer.color = keypadColor;
    }

    public void OnCorrectCode()
    {
        if (this.isPermanent) this.isSolved = true;
    }

    public override void OnInteract(PlayerController player)
    {
        base.OnInteract(player);

        KeypadScreen.Open(this);
    }
}