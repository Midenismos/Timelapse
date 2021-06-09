using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{

    public GameObject Destination;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Destination.transform.position, 10 * Time.deltaTime);
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
