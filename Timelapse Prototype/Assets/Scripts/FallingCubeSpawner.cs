using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCubeSpawner : MonoBehaviour
{
    [SerializeField] private Transform cube = null;
    [SerializeField] private float respawnTimer = 8;

    private TimeManager timeManager = null;
    private float counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = cube.position;

        timeManager = FindObjectOfType<TimeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime * timeManager.multiplier;

        if(counter >= respawnTimer)
        {
            cube.transform.position = transform.position;
            counter = 0;
        }
    }
}
