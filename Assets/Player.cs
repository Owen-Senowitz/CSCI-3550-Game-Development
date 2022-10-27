using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * 15 * Time.deltaTime, 0f, 0f);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float jumpVelocity = 10f;
            rigidbody2d.velocity = Vector2.up * jumpVelocity;
        }
    }
}
