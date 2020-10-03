using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public Rigidbody2D rigidBody;
    public Animator animator;

    public bool IsWalking => this.movement.magnitude > 0;

    private Vector2 movement;
    private Vector2 previousMovement;

    private Vector3 targetPosition;

    void Update()
    {
        // INPUT
        this.movement.x = Input.GetAxisRaw("Horizontal");
        this.movement.y = Input.GetAxisRaw("Vertical");
        this.movement.Normalize();

        if (this.movement.sqrMagnitude > 0)
        {
            this.previousMovement = this.movement;

            //this.animator.SetFloat("PreviousHorizontal", this.previousMovement.x);
            //this.animator.SetFloat("PreviousVertical", this.previousMovement.y);
        }

        //this.animator.SetFloat("Horizontal", this.movement.x);
        //this.animator.SetFloat("Vertical", this.movement.y);

        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = transform.position.z;

        Vector3 deltaPosition = targetPosition - transform.position;
        Vector3 targetDirection = deltaPosition.normalized;

        animator.SetFloat("Horizontal", targetDirection.x);
        animator.SetFloat("Vertical", targetDirection.y);
        this.animator.SetFloat("Speed", this.movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        this.rigidBody.MovePosition(this.rigidBody.position + this.movement * this.movementSpeed * Time.fixedDeltaTime);
    }
}
