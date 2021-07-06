using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaledPhysicsObject : MonoBehaviour, ITimeStoppable
{
    private Rigidbody body = null;

    private void Start()
    {
        body = GetComponent<Rigidbody>();

        TimeManager timeManager = FindObjectOfType<TimeManager>();
        if (timeManager)
            timeManager.RegisterTimeStoppable(this);
    }
    public void StartTimeStop()
    {
        body.isKinematic = true;
    }

    public void EndTimeStop()
    {
        body.isKinematic = false;
    }

    private void OnDestroy()
    {
        TimeManager timeManager = FindObjectOfType<TimeManager>();
        if (timeManager)
            timeManager.RegisterTimeStoppable(this);
    }
}
