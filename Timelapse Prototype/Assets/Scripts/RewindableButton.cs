using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindableButton : Rewindable
{
    [SerializeField] private Button button = null;
    public override void StartRewind()
    {
        base.StartRewind();
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
