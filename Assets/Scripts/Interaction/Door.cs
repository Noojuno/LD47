using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator;
    public GameObject lockIcon;
    public GameObject oneWayIcon;
    public GameObject bothIcons;
    public bool stayOpen = false;
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
                this.Open(false);
            }
        }
    }

    public void SetUnlocked(bool unlocked)
    {
        this.isLocked = !unlocked;
    }

    public void Open(bool open)
    {
        if (this.isOpen && open && this.stayOpen) return;

        this.isOpen = open;
        this.animator.SetBool("Open", open);
    }

    //public void OnInteract(GameObject interactor)
    //{
    //    this.Open(!this.isOpen);
    //}
}