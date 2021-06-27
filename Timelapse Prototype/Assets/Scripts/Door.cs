using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Rewindable, ITimeStoppable
{
    [SerializeField] private Animator animator = null;
    public void StartTimeStop()
    {
        animator.enabled = false;
    }

    public void EndTimeStop()
    {
        animator.enabled = true;
    }

    public override void StartRewind()
    {
        base.StartRewind();
    }

    public override void EndRewind()
    {
        base.EndRewind();
    }
}
