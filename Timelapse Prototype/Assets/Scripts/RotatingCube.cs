using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCube : MonoBehaviour, ITimeStoppable
{
    //Touchez à cette variable pour le LD
    [SerializeField]
    private float BaseSpeed = 5;

    //Ne pas toucher à ces variables
    public float multiplier = 1f;
    private TimeManager timeManager;

    private bool isStopped = false;

    // Start is called before the first frame update
    void Start()
    {
        //Connecte l'objet au TimeManager
        timeManager = FindObjectOfType<TimeManager>();

        if (timeManager)
            timeManager.RegisterTimeStoppable(this);
    }

    // Update is called once per frame
    void Update()
    {
        //Change la vitesse du gameObject

        //Tourne l'item en fonction du multiplier
        //OLD CODE : gameObject.transform.Rotate(0, BaseSpeed * multiplier, 0);
        if(!isStopped)
            Rotate(Time.deltaTime);
    }

    public void Rotate(float deltaGameTime)
    {
        gameObject.transform.Rotate(0, BaseSpeed * deltaGameTime, 0);
    }

    public void StartTimeStop()
    {
        isStopped = true;
    }
    public void EndTimeStop()
    {
        isStopped = false;
    }

    private void OnDestroy()
    {
        if (timeManager)
            timeManager.UnRegisterTimeStoppable(this);
    }

}
