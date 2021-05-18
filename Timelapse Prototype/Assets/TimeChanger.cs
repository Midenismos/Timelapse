using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChanger : MonoBehaviour
{
    //Changer ces variables pour équilibrage
    [SerializeField]
    private float Duration = 0.0f;

    [SerializeField]
    private float NewMultiplier = 0.0f;

    //Ne pas changer ces variables
    private GameObject TimeManager;


    // Start is called before the first frame update
    void Start()
    {
        //Connecte l'objet au TimeManager
        TimeManager = GameObject.Find("TimeManager");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeTime()
    {
        //Initialise le timer du TimeManager
        TimeManager.GetComponent<TimeManager>().timer = Duration;
        //Change le multiplier de vitesse
        TimeManager.GetComponent<TimeManager>().StartTimeChange(NewMultiplier);
    }
}
