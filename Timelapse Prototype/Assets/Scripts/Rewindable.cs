﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewindable : MonoBehaviour
{
    public virtual void StartRewind()
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
}
