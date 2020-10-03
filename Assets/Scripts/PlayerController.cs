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
    private bool facingRight = true;

    void Awake()
    {
        this.rigidBody = this.GetComponent<Rigidbody2D>();
        this.animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        this.previousMovement = this.movement;

        // INPUT
        this.movement.x = Input.GetAxisRaw("Horizontal");
        this.movement.y = Input.GetAxisRaw("Vertical");
        this.movement.Normalize();

        if (this.movement.x > 0 || this.movement.x < 0) this.Flip(this.movement.x > 0);

        this.animator.SetBool("Walking", this.IsWalking);
    }

    void FixedUpdate()
    {
        this.rigidBody.MovePosition(this.rigidBody.position + this.movement * this.movementSpeed * Time.fixedDeltaTime);
    }

    void Flip(bool right)
    {
        if (right != this.facingRight)
        {
            Vector3 scale = this.transform.localScale;
            scale.x *= -1;
            this.transform.localScale = scale;

            this.facingRight = right;
        }
    }
}
