using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindablePhysicsObject : Rewindable
{
    struct TimeStampedTransform
    {
        public float timeStamp;
        public Vector3 position;
        public Quaternion rotation;

        public TimeStampedTransform(float timeStamp, Vector3 position, Quaternion rotation)
        {
            this.timeStamp = timeStamp;
            this.position = position;
            this.rotation = rotation;
        }
    }
       
    [SerializeField] private Rigidbody body = null;

    private List<TimeStampedTransform> history = new List<TimeStampedTransform>();

    public override void StartRewind(float timestamp)
    {
        base.StartRewind(timestamp);
        body.useGravity = false;

        Record(timestamp);
    }

    public override void Rewind(float deltaGameTime, float totalTime)
    {
        base.Rewind(deltaGameTime, totalTime);

        RewindHistory(totalTime);
        if(history.Count > 1)
        {
            float t = (1 - (totalTime - history[1].timeStamp)) / history[0].timeStamp - history[1].timeStamp;

            transform.position = Vector3.Lerp(history[0].position, history[1].position, t);
            transform.rotation = Quaternion.Lerp(history[0].rotation, history[1].rotation, t);
        }
        
    }

    public override void EndRewind()
    {
        base.EndRewind();
        body.useGravity = true;
    }

    public override void Record(float timeStamp)
    {
        history.Insert(0, new TimeStampedTransform(
            timeStamp,
            transform.position,
            transform.rotation));

    }

    private void RewindHistory(float timeStamp)
    {
        while (history.Count > 2)
        {
            if(history[0].timeStamp >= timeStamp && history[1].timeStamp < timeStamp)
            {
                return;
            }

            history.RemoveAt(0);
        }
    }
}
