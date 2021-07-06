using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Rewindable, ITimeStoppable
{
    [SerializeField] private Animator animator = null;

    private void Start()
    {
        TimeManager timeManager = FindObjectOfType<TimeManager>();
        if (timeManager)
            timeManager.RegisterTimeStoppable(this);
    }
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

    private void OnDestroy()
    {
        TimeManager timeManager = FindObjectOfType<TimeManager>();
        if (timeManager)
            timeManager.RegisterTimeStoppable(this);
    }
}
