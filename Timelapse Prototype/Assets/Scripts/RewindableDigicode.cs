using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindableDigicode : Rewindable
{

    [SerializeField] private Digicode digicode = null;
    public override void StartRewind(float timestamp)
    {
        base.StartRewind(timestamp);
    }

    public override void Rewind(float deltaGameTime, float totalTime)
    {
        base.Rewind(deltaGameTime, totalTime);
        digicode.timerBlink += deltaGameTime;
    }
}

