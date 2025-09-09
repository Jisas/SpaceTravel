using UnityEngine;

public class CameraStarterPos : MonoBehaviour
{
    private Transform myTransform;

    void Start()
    {
        myTransform = GetComponent<Transform>();
        myTransform.position = new Vector3(0, 165, -305); 
    }
}
