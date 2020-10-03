using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButton : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public bool triggered = false;

    public Sprite inactiveSprite;
    public Sprite activeSprite;

    void OnTriggerEnter2D(Collider2D col)
    {
        this.triggered = true;

        this.spriteRenderer.sprite = activeSprite;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        this.triggered = false;

        this.spriteRenderer.sprite = inactiveSprite;
    }
}
