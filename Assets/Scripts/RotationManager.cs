using UnityEngine;

public class RotationManager : MonoBehaviour
{
    private float rotationZ;
    public float rotationSpeed;
    public bool clockwiseRotation;

    void Update()
    {
        if(clockwiseRotation == false)
        {
            rotationZ += Time.deltaTime * rotationSpeed;
        }
        else
        {
            rotationZ -= Time.deltaTime * rotationSpeed;
        }

        transform.rotation = Quaternion.Euler(0, 0, rotationZ);
    }
}
