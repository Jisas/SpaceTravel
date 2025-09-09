using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public float speed = 5f;
    public bool movXNegative = false;
    public bool movXPositive = false;
    public bool movYNegative = false;
    public bool movYPositive = false;
    public bool movZNegative = false;
    public bool movZPositive = false;

    void Update()
    {
        if(movXNegative == true)
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }
        if (movXPositive == true)
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }

        if (movYNegative == true)
        {
            transform.position -= transform.up * speed * Time.deltaTime;
        }
        if (movYPositive == true)
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }

        if (movZNegative == true)
        {
            transform.position -= transform.forward * speed * Time.deltaTime;
        }
        if (movZPositive == true)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }
}
