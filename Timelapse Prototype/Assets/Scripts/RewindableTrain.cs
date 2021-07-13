using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindableTrain : Rewindable
{
    [SerializeField] private Train train = null;

    public override void StartRewind(float timestamp)
    {
        base.StartRewind(timestamp);
    }

    public override void Rewind(float deltaGameTime, float totalTime)
    {
        base.Rewind(deltaGameTime, totalTime);
        


        if (train.transform.position == train.Destination[train.previousDestination].transform.position)
        {
            train.currentDestination = train.previousDestination;
            train.previousDestination -= 1;
            if (train.previousDestination == -1)
            {
                train.previousDestination = train.Destination.Length - 1;
            }
        }
        train.transform.position = Vector3.MoveTowards(transform.position, train.Destination[train.previousDestination].transform.position, train.speed * deltaGameTime);
        

    }
}
