using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Powered
{
    public AudioSource audioSource;
    public Animator animator;
    public GameObject lockIcon;
    public GameObject oneWayIcon;
    public GameObject bothIcons;
    public bool isLocked;
    public bool isOpen;
    public bool isAutomatic;
    public bool isOneWay;
    public float automaticOpenRange = 0.6f;

    void Update()
    {
        if (this.lockIcon != null) this.lockIcon.SetActive(this.isLocked && !this.isOneWay && !this.isOpen);
        if (this.oneWayIcon != null) this.oneWayIcon.SetActive(this.isOneWay && !this.isLocked && !this.isOpen);
        if (this.bothIcons != null) this.bothIcons.SetActive(this.isOneWay && this.isLocked && !this.isOpen);

        if (this.isAutomatic)
        {
            var player = GameManager.Instance.Player;
            var dist = Vector3.Distance(this.transform.position, player.transform.position);

            if (dist <= this.automaticOpenRange && (!this.isOneWay || player.transform.position.y <= this.transform.position.y + 0.05))
            {
                if (!this.isOpen) this.Open(true);
            }
            else
            {
                if (this.isOpen) this.Open(false);
            }
        }
    }

    public void SetUnlocked(bool unlocked)
    {
        this.isLocked = !unlocked;
    }

    public void Open(bool open)
    {
        if (!open && this.isLocked && this.isOpen) return;

        this.isOpen = open;
        this.animator.SetBool("Open", open);

        this.audioSource.Play();
    }

    public override void OnPowered()
    {
        base.OnPowered();

        this.Open(true);
    }

    public override void OnUnpowered()
    {
        base.OnUnpowered();

        this.Open(false);
    }

    /* void OnTriggerEnter2D(Collider2D collider)
    {
        if (!this.isAutomatic) return;
        if (collider.isTrigger) return;
        if (collider.gameObject != GameManager.Instance.Player?.gameObject) return;

        this.Open(true);
    } */
    //public void OnInteract(GameObject interactor)
    //{
    //    this.Open(!this.isOpen);
    //}
}