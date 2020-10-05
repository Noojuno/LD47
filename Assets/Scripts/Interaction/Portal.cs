using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Interactable
{
    public float loopDuration;

    public bool isFirst = false;
    public bool endPortal = false;

    void Awake()
    {
        if (this.isFirst)
        {
            this.SetupLoop();
        }
    }

    void SetupLoop()
    {
        GameManager.Instance.loopPortal = this;
        GameManager.Instance.SetLoopLength(this.loopDuration);
        GameManager.Instance.SetTimeRemaining(this.loopDuration);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger) return;
        if (collision.gameObject != GameManager.Instance.Player.gameObject) return;

        GameManager.Instance.SetTimeRunning(true);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger) return;
        if (collision.gameObject != GameManager.Instance.Player.gameObject) return;
        
        GameManager.Instance.SetTimeRunning(false);
    }

    public override void OnSelect(PlayerController player)
    {
        if (this == GameManager.Instance.loopPortal) return;

        base.OnSelect(player);
    }

    public override void OnDeselect(PlayerController player)
    {
        if (this == GameManager.Instance.loopPortal) return;

        base.OnDeselect(player);
    }

    public override void OnInteract(PlayerController player)
    {
        base.OnInteract(player);

        this.OnDeselect(player);

        if (this != GameManager.Instance.loopPortal)
        {
            if (this.endPortal)
            {
                // LOAD NEXT LEVEL
            }
            else
            {
                this.SetupLoop();
            }
        }
    }
}
