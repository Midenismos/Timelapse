﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestVictoryDigicode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void DeActivate()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
