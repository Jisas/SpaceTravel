using UnityEngine;

public class VelocityTracker : MonoBehaviour
{
    public float Speed { get; private set; }

    private ObjectsMove mover;

    private void OnEnable()
    {
        mover = GetComponent<ObjectsMove>();
        Speed = mover.GetSpeed();
        mover.SetSpeed(Speed);
    }

    private void OnDisable()
    {
        Speed = mover.GetSpeed();
    }

    public void IncreaseSpeed(float amount)
    {
        Speed += amount;
        mover.SetSpeed(Speed);
    }
}