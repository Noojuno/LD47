using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator;
    public GameObject lockIcon;
    public bool stayOpen = false;
    public bool isLocked;
    public bool isOpen;
    public bool isAutomatic;
    public bool isOneWay;
    public float automaticOpenRange = 0.6f;

    private PlayerController player;

    void Start()
    {
        GameObject gameObj = GameObject.FindGameObjectWithTag("Player");

        if (gameObj != null && gameObj.GetComponent<PlayerController>() != null)
        {
            this.player = gameObj.GetComponent<PlayerController>();
        }
    }

    void Update()
    {
        this.lockIcon.SetActive(this.isLocked && !this.isOpen);

        if (this.isAutomatic)
        {
            var dist = Vector3.Distance(this.transform.position, this.player.transform.position);

            if (dist <= this.automaticOpenRange && (!this.isOneWay || this.player.transform.position.y <= this.transform.position.y + 0.15))
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