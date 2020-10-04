using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FloorButton : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Sprite inactiveSprite;
    public Sprite activeSprite;

    public bool triggered = false;
    public UnityEvent<bool> triggerEvent;

    private List<Collider2D> currentColliders = new List<Collider2D>();

    void Start()
    {
        if (this.triggerEvent == null)
        {
            this.triggerEvent = new UnityEvent<bool>();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (this.currentColliders.Count <= 0)
        {
            this.triggered = true;
            this.spriteRenderer.sprite = activeSprite;
            this.triggerEvent.Invoke(true);
        }

        this.currentColliders.Add(col);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        this.currentColliders.Remove(col);

        if (this.currentColliders.Count <= 0)
        {
            this.triggered = false;
            this.spriteRenderer.sprite = inactiveSprite;
            this.triggerEvent.Invoke(false);
        }
    }
}
