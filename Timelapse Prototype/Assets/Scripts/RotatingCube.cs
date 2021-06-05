using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCube : MonoBehaviour
{
    //Touchez à cette variable pour le LD
    [SerializeField]
    private float BaseSpeed = 5;

    //Ne pas toucher à ces variables
    public float multiplier = 1f;
    private TimeManager timeManager;

    // Start is called before the first frame update
    void Start()
    {
        //Connecte l'objet au TimeManager
        timeManager = FindObjectOfType<TimeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Change la vitesse du gameObject

        //Tourne l'item en fonction du multiplier
        //OLD CODE : gameObject.transform.Rotate(0, BaseSpeed * multiplier, 0);
        Rotate(Time.deltaTime * timeManager.multiplier);
    }

    public void Rotate(float deltaGameTime)
    {
        gameObject.transform.Rotate(0, BaseSpeed * deltaGameTime, 0);
    }
}
