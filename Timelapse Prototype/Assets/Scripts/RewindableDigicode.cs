using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindableDigicode : Rewindable
{

    [SerializeField] private Digicode digicode = null;
    public override void StartRewind()
    {
        base.StartRewind();
    }

    public override void Rewind(float deltaGameTime, float totalTime)
    {
        base.Rewind(deltaGameTime, totalTime);
        digicode.timerBlink += deltaGameTime;
    }
}

