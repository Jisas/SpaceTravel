using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    public float speedRot;
    public float maxTouchRadius;
    [SerializeField]private Quaternion inicialRot;

    void Start()
    {
        inicialRot = this.transform.rotation;
    }

    async void Update()
    {
        if(Input.touchCount > 0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if(Input.GetTouch(0).radius < maxTouchRadius)
                {
                    Vector3 touchPos = Input.GetTouch(0).deltaPosition;
                    float x = touchPos.x * Mathf.Deg2Rad * speedRot;
                    float y = touchPos.y * Mathf.Deg2Rad * speedRot;
                    transform.Rotate(Vector3.up, -x);
                    transform.Rotate(Vector3.forward, -y);

                }    
            } else if(Input.GetTouch(0).phase == TouchPhase.Ended) this.transform.rotation = inicialRot;
        } 
        else 
        { 
            await Task.Yield();
        }      
    }
}
