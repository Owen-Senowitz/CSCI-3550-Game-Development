using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spearmanDamage : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private float checkTimer;

    private Vector3 destination;
    private bool attacking;

    private Vector3[] directions = new Vector3[1];


    private void Update()
    {
        if (attacking)
        {
            transform.Translate(destination * Time.deltaTime * speed);
        }
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
            {
                CheckForPlayer();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
        Stop();
    }
    private void CheckForPlayer()
    {
        CalculateDirection();

        for(int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if(hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }
    private void CalculateDirection()
    {
        directions[0] = -transform.right * range;
        //directions[1] = transform.right * range;
        //directions[2] = transform.up * range;
       // directions[3] = -transform.up * range;


    }
    private void Stop()
    {
        destination = transform.position;
        attacking = false;
    }

    private void OnEnable()
    {
        Stop();
    }
}
