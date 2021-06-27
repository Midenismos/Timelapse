using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaledPhysicsObject : MonoBehaviour, ITimeStoppable
{
    private Rigidbody body = null;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }
    public void StartTimeStop()
    {
        body.isKinematic = true;
    }

    public void EndTimeStop()
    {
        body.isKinematic = false;
    }
}
