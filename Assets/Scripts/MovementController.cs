using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    BoxCollider2D boxCollider2D;

    private Vector3 respawnPoint;
    

    string animationState = "AnimationState";

    private float timeBtwAttack;
    public float startTimeAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;

    enum CharStates
    {
        idle = 0,
        walk = 1,
        attacking = 2
    }

    // use this for initialization
    private void Start()
    {
        // get references to game object component so it doesn't have to be grabbed each time needed
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        respawnPoint = transform.position;
        boxCollider2D = GetComponent<BoxCollider2D>();
        Debug.Log(boxCollider2D.size);
    }

    // called once per frame
    private void Update()
    {
        UpdateState();
    }


    void UpdateState()
    {
        // if attacking
        if (Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.Mouse0))
        {
            Debug.Log("Attack");
            DealDamage();
            animator.SetInteger(animationState, (int)CharStates.attacking);
        }
        // if not attacking
        else if (!(Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.Mouse0)))
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
            else
            {
                animator.SetInteger(animationState, (int)CharStates.idle);
            }
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
        //movement.y = Input.GetAxisRaw("Vertical");
        

        // keeps player moving at the same rate of speed, no matter which direction they are moving in
        movement.Normalize();

        // set velocity of RigidBody2D and move it
        rb2D.velocity = movement * movementSpeed;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            SceneManager.LoadScene("project");
            //transform.position = respawnPoint;

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    private void DealDamage()
    {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                Debug.Log(enemiesToDamage.ToString());
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    Debug.Log(enemiesToDamage[i].ToString());
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                }
            }
            timeBtwAttack = startTimeAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }
}