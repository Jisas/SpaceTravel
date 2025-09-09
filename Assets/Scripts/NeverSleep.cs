using UnityEngine;

public class NeverSleep : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 120;
    }

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
