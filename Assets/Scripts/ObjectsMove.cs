using UnityEngine;

public class ObjectsMove : MonoBehaviour
{
    [SerializeField] private float startSpeed = 5f;
    [SerializeField] private bool eulerObject = false;
    [SerializeField] private bool negativeZ = true;
    [SerializeField] private bool positiveZ = false;

    private VelocityTracker tracker;
    [SerializeField] private float currentSpeed;
    private const float velocityFactor = 0.01f;

    private bool HasVelocityTraker
    {        
        get => TryGetComponent<VelocityTracker>(out tracker);
    }

    public void SetSpeed(float speed)
    {
        this.currentSpeed = speed;
    }

    public float GetSpeed()
    {
        return currentSpeed;
    }

    private void Awake()
    {
        currentSpeed = startSpeed;
    }

    void Update()
    {
        if (HasVelocityTraker) tracker.IncreaseSpeed(velocityFactor);

        if(positiveZ == true)
        {
            if(eulerObject == true)
            {
                transform.position -= currentSpeed * Time.deltaTime * transform.up;
            }
            else
            {
                transform.position += currentSpeed * Time.deltaTime * transform.forward;
            }
        }

        if(negativeZ == true)
        {
            if(eulerObject == true)
            {
                transform.position += currentSpeed * Time.deltaTime * transform.up; 
            }
            else 
            {
                transform.position -= currentSpeed * Time.deltaTime * transform.forward;
            }
        }
    }
}
