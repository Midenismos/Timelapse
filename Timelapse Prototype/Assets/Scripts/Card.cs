using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool isBroken = false;
    public GameObject whole;
    public GameObject[] broken = new GameObject[2];
    
    [SerializeField]
    public float timerSinceBroken = 0.0f;
    public float multiplier = 1f;

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
        multiplier = TimeManager.GetComponent<TimeManager>().multiplier;

        //Fait apparaître le gameObject Whole si la carte n'est pas cassée
        if (isBroken == false)
        {
            foreach (GameObject brokenPart in broken)
            {
                brokenPart.gameObject.SetActive(false);
                whole.gameObject.SetActive(true);
            }
        }
        // Retire le gameObject Whole et fait apparaître les deux gameObject Broken si la carte est cassée
        else
        {
            foreach (GameObject brokenPart in broken)
            {
                brokenPart.gameObject.SetActive(true);
                whole.gameObject.SetActive(false);
            }

            //Calcule le temps passé depuis que la carte a été cassée, pour l'instant ça calcule en seconde réelle
            timerSinceBroken += Time.deltaTime * multiplier;
        }

        // Régénère la carte si le timer est inférieur à 0 (après retour dans le temps)
        if (timerSinceBroken < 0)
        {
            isBroken = false;
        }
    }

}
