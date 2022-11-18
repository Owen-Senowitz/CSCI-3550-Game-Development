using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // public variables appear as properties in Unity's inspector window
    public float movementSpeed = 3.0f;

    // holds 2D points; used to represent a character's location in 2D space, or where it's moving to
    Vector2 movement = new Vector2();

    // reference to the character's Rigidbody2D component
    Rigidbody2D rb2D;
    Animator animator;
    SpriteRenderer spriteRenderer;
    

    string animationState = "AnimationState";

    enum CharStates
    {
        walk = 1,
        idle = 0
    }

    // use this for initialization
    private void Start()
    {
        // get references to game object component so it doesn't have to be grabbed each time needed
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // called once per frame
    private void Update()
    {
        UpdateState();
    }

    void UpdateState()
    {
        if (movement.x < 0)
        {
            animator.SetInteger(animationState, (int)CharStates.walk);
            spriteRenderer.flipX = true;
        }
        else if (movement.x > 0)
        {
            animator.SetInteger(animationState, (int)CharStates.walk);
            spriteRenderer.flipX = false;

        }
        
        else if (movement.y < 0)
        {
            animator.SetInteger(animationState, (int)CharStates.walk);

        }
        else if (movement.y > 0)
        {
            animator.SetInteger(animationState, (int)CharStates.walk);

        }
        
        else
        {
            animator.SetInteger(animationState, (int)CharStates.idle);
        }
    }

    // called at fixed intervals by the Unity engine
    // update may be called less frequently on slower hardware when frame rate slows down
    void FixedUpdate()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        // get user input
        // GetAxisRaw parameter allows us to specify which axis we're interested in
        // Returns 1 = right key or "d" (up key or "w")
        //        -1 = left key or "a"  (down key or "s")
        //         0 = no key pressed
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        

        // keeps player moving at the same rate of speed, no matter which direction they are moving in
        movement.Normalize();

        // set velocity of RigidBody2D and move it
        rb2D.velocity = movement * movementSpeed;
    }
}