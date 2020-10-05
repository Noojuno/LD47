using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class FloorButton : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Sprite inactiveSprite;
    public Sprite activeSprite;

    public bool triggered = false;
    public UnityEvent<bool> OnPressEvent;
    public UnityEvent<bool> OnDepressEvent;

    private List<Collider2D> currentColliders = new List<Collider2D>();

    void Start()
    {
        if (this.OnPressEvent == null)
        {
            this.OnPressEvent = new UnityEvent<bool>();
        }

        if (this.OnDepressEvent == null)
        {
            this.OnDepressEvent = new UnityEvent<bool>();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger) return;

        if (this.currentColliders.Count <= 0)
        {
            this.triggered = true;
            this.spriteRenderer.sprite = activeSprite;
            this.OnPressEvent.Invoke(true);
        }

        this.currentColliders.Add(col);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.isTrigger) return;

        this.currentColliders.Remove(col);

        if (this.currentColliders.Count <= 0)
        {
            this.triggered = false;
            this.spriteRenderer.sprite = inactiveSprite;
            this.OnDepressEvent.Invoke(false);
        }
    }
}
