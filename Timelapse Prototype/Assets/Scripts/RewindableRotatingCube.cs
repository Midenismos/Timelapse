using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindableRotatingCube : Rewindable
{
    [SerializeField] private RotatingCube rotatingCube = null;
    public override void StartRewind(float timestamp)
    {
        base.StartRewind(timestamp);
    }

    public override void Rewind(float deltaGameTime, float totalTime)
    {
        base.Rewind(deltaGameTime, totalTime);
        rotatingCube.Rotate(-deltaGameTime);
    }
}
