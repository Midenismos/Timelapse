using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public GameObject[] Destination;

    public int currentDestination = 0;
    public int previousDestination = 0;

    public float speed = 0;

    public float multiplier = 1f;

    private GameObject TimeManager;

    // Start is called before the first frame update
    void Start()
    {
        //Connecte l'objet au TimeManager
        TimeManager = GameObject.Find("TimeManager");
        previousDestination = Destination.Length-1;
    }

    // Update is called once per frame
    void Update()
    {
        multiplier = TimeManager.GetComponent<TimeManager>().multiplier;



        if (transform.position == Destination[currentDestination].transform.position)
        {
            previousDestination = currentDestination;
            currentDestination += 1;
            if (currentDestination == Destination.Length)
            {
                currentDestination = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, Destination[currentDestination].transform.position, speed * multiplier * Time.deltaTime);
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            other.transform.parent = transform;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            other.transform.parent = null;
        }
    }
}
