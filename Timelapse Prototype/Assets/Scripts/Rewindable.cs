using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewindable : MonoBehaviour
{
    private void Start()
    {
        RewindManager rewindManager = FindObjectOfType<RewindManager>();
        if(rewindManager)
        {
            rewindManager.RegisterRewindable(this);
        }
    }
    public virtual void StartRewind(float timeStamp)
    {
    }

    public virtual void Rewind(float deltaGameTime, float timeStamp)
    {

    }

    public virtual void EndRewind()
    {

    }

    public virtual void Record(float timeStamp)
    {

    }

    private void OnDestroy()
    {
        RewindManager rewindManager = FindObjectOfType<RewindManager>();
        if (rewindManager)
        {
            rewindManager.UnRegisterRewindable(this);
        }
    }
}
