using UnityEngine;

public class Scale : MonoBehaviour
{
    [Header("FUNCTION")]
    public bool scale;
    public bool unescale;

    [Header("VARIABLES")]
    public float multiplierScale;
    public float scaleSpeed;
    public float scaleProportion;

    void Update()
    {
        if(scale == true)
        {
            GetScale();
        }
        else if(unescale == true)
        {
            GetUnescale();
        }
    }

    public void GetScale()
    {
        transform.localScale += new Vector3
        (multiplierScale * scaleSpeed / scaleProportion, 
        multiplierScale * scaleSpeed / scaleProportion, 
        multiplierScale * scaleSpeed / scaleProportion) * Time.deltaTime;
    }

    public void GetUnescale()
    {
        transform.localScale -= new Vector3
        (multiplierScale * scaleSpeed / scaleProportion,
        multiplierScale * scaleSpeed / scaleProportion,
        multiplierScale * scaleSpeed / scaleProportion) * Time.deltaTime;
    }

}
