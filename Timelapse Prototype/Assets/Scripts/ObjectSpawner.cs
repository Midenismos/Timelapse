using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn = null;

    private GameObject objectInstance = null;

    public void SpawnObject()
    {
        if (objectInstance)
        {
            Destroy(objectInstance);
        }

        objectInstance = Instantiate(objectToSpawn, transform.position, transform.rotation);
    }
}
