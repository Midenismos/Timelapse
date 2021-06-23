using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool clicked = false;

    public float timerSinceClicked = 0.0f;

    private GameObject TimeManager;
    public float multiplier = 1f;

    public Material buttonActivatedMaterial = null;
    public Material buttonDeactivatedMaterial = null;

    public GameObject rewindedActionObject = null;
    public string rewindedFunction;


    // Start is called before the first frame update
    void Start()
    {
        TimeManager = GameObject.Find("TimeManager");
    }

    // Update is called once per frame
    void Update()
    {
        multiplier = TimeManager.GetComponent<TimeManager>().multiplier;

        if (clicked == true)
        {
            timerSinceClicked += Time.deltaTime * multiplier;
            GetComponent<MeshRenderer>().material = buttonActivatedMaterial;
        }
        else
        {
            GetComponent<MeshRenderer>().material = buttonDeactivatedMaterial;
        }
        if (timerSinceClicked < 0)
        {
            clicked = false;
            rewindedActionObject.SendMessage(rewindedFunction);
            timerSinceClicked = 0;
        }

    }
}
