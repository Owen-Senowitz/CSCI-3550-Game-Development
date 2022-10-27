using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    void Update ()
    {
        transform.Translate(Input.GetAxis("Horizontal")* 15 * Time.deltaTime, 0f, 0f);
    }
}
