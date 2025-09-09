using UnityEngine;

public class RotateManager : MonoBehaviour
{
    public float speed;
    public bool clockwiseRotation = false;
    public bool rotateInX = false;
    public bool rotateInY = false;
    public bool rotateInZ = false;
    float formuleRotate;

    void Update()
    {

        if (clockwiseRotation == false)
        {
            formuleRotate = Time.deltaTime * speed;
        }
        else
        {
            formuleRotate = - Time.deltaTime * speed;
        }

        if(rotateInX == true)
        {
            transform.Rotate(formuleRotate, 0, 0);
        }

        if (rotateInY == true)
        {
            transform.Rotate(0, formuleRotate, 0);
        }

        if (rotateInZ == true)
        {
            transform.Rotate(0, 0, formuleRotate);
        }
    }
}
