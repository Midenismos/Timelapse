using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindableButton : Rewindable
{
    [SerializeField] private Button button = null;
    public override void StartRewind(float timestamp)
    {
        base.StartRewind(timestamp);
    }

    public override void Rewind(float deltaGameTime, float totalTime)
    {
        base.Rewind(deltaGameTime, totalTime);
        if (button.clicked == true)
        {
            button.timerSinceClicked -= deltaGameTime;
        }
    }
}
