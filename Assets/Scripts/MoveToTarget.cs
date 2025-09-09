using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    public Transform targetPoint;
    public float speed;
    private readonly float threshold = 0.1f;
    private bool inicialized = false;

    public void Initialize(Transform targetPoint, float speed)
    {
        this.targetPoint = targetPoint;
        this.speed = speed;
        inicialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        var distance = Vector3.Distance(transform.position, targetPoint.position);
        if (inicialized && distance > threshold)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, step);
        }
    }
}
