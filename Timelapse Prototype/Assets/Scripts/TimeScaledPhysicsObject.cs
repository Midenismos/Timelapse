using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaledPhysicsObject : MonoBehaviour, ITimeStoppable
{
    private Rigidbody body = null;
    [SerializeField] private AudioSource impactSound;

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude >1)
        {
            if (impactSound)
            {
                impactSound.Play();
            }
        }
    }

}
