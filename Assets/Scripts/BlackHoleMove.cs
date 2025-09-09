using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleMove : MonoBehaviour
{
    public float speed = 5f;
    public bool eulerObject = false;
    public bool negativeZ = true;
    public bool positiveZ = false;
    
    void Update()
    {
        if(positiveZ == true)
        {
            if(eulerObject == true)
            {
                transform.position -= transform.up * speed * Time.deltaTime;
            }
            else
            {
                transform.position += transform.forward * speed * Time.deltaTime;
            }
        }

        if(negativeZ == true)
        {
            if(eulerObject == true)
            {
                transform.position += transform.up * speed * Time.deltaTime; 
            }
            else 
            {
                transform.position -= transform.forward * speed * Time.deltaTime;
            }
        }
    }
}
