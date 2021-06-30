using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn = null;

    public void SpawnObject()
    {
        GameObject spawned = Instantiate(objectToSpawn, transform.position, transform.rotation);
    }
}
