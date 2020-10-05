using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rigidBody;
    public Animator animator;

    [Header("Variables")]
    public float movementSpeed = 5f;
    public float holdSpeedMultiplier = 0.8f;
    public bool movementEnabled = true;

    public Transform holdPosition;
    public GameObject holding;

    public Vector2 movement;
    public Vector2 facing;

    private Interactable selectedInteractable;
    private float existingInteractDistance = 100f;

    void Update()
    {
        if (this.movementEnabled) this.DoMovement();
        this.DoHolding();
        this.DoInput();
    }

    void DoInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            this.selectedInteractable?.OnInteract(this);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameManager.Instance.Loop();
        }
    }

    void DoHolding()
    {
        if (this.holding != null)
        {
            this.holding.transform.position = this.holdPosition.position;
        }
    }

    void DoMovement()
    {
        // INPUT
        this.movement.x = Input.GetAxisRaw("Horizontal");
        this.movement.y = Input.GetAxisRaw("Vertical");
        this.movement.Normalize();

        if (this.movement.sqrMagnitude > 0)
        {
            this.facing = this.movement;
        }

        this.animator.SetFloat("FacingHorizontal", this.facing.x);
        this.animator.SetFloat("FacingVertical", this.facing.y);

        this.animator.SetFloat("Horizontal", this.movement.x);
        this.animator.SetFloat("Vertical", this.movement.y);
        this.animator.SetFloat("Speed", this.movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        float moveSpeed = this.movementSpeed;

        if (this.holding != null) moveSpeed *= this.holdSpeedMultiplier;

        if (this.movementEnabled) this.rigidBody.MovePosition(this.rigidBody.position + this.movement * moveSpeed * Time.fixedDeltaTime);
    }
    
    public void Hold(GameObject obj)
    {
        if (this.holding != null && this.holding != obj)
        {
            if (this.holding.GetComponent<Rigidbody2D>())
            {
                this.holding.GetComponent<Rigidbody2D>().simulated = true;
            }
        }

        this.holding = obj;

        if (this.holding != null)
        {
            if (this.holding.GetComponent<Rigidbody2D>())
            {
                this.holding.GetComponent<Rigidbody2D>().simulated = false;
            }
        }
    }

    void CalculateNearestInteractable(GameObject collider)
    {
        //if (collider.gameObject.layer != LayerMask.NameToLayer("Interactable")) return;

        if (this.holding != null) return;

        float distance = Vector3.Distance(this.transform.position, collider.transform.position);
        if (distance >= this.existingInteractDistance) return;

        var interactable = collider.gameObject.GetComponent<Interactable>();

        if (interactable != null)
        {
            if (this.selectedInteractable != null)
            {
                this.selectedInteractable?.OnDeselect(this);
            }

            this.selectedInteractable = interactable;
            this.existingInteractDistance = distance;
            this.selectedInteractable?.OnSelect(this);
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject == this.selectedInteractable?.gameObject) return;

        this.CalculateNearestInteractable(collider.gameObject);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject != this.selectedInteractable?.gameObject) return;

        this.selectedInteractable?.OnDeselect(this);
        this.existingInteractDistance = 100f;

        if (this.selectedInteractable.gameObject != this.holding) this.selectedInteractable = null;
    }
}