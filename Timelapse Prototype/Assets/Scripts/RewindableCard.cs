using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindableCard : Rewindable
{
    [SerializeField] private Card card = null;
    public override void StartRewind()
    {
        base.StartRewind();
    }

    public override void Rewind(float deltaGameTime, float totalTime)
    {
        base.Rewind(deltaGameTime, totalTime);
        
        // Fait reculer le temps passé depuis que la carte a été cassée
        if (card.isBroken == true)
        {
            card.timerSinceBroken -= deltaGameTime;
        }
    }
}
