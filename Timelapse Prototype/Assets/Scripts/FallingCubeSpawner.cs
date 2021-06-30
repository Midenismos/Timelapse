using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCubeSpawner : MonoBehaviour, ITimeStoppable
{
    [SerializeField] private Transform cube = null;
    [SerializeField] private float respawnTimer = 8;

    private TimeManager timeManager = null;
    private float counter = 0;

    private bool isStopped = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = cube.position;

        timeManager = FindObjectOfType<TimeManager>();

        if (timeManager)
            timeManager.RegisterTimeStoppable(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (isStopped) return;

        counter += Time.deltaTime * timeManager.multiplier;

        if(counter >= respawnTimer)
        {
            cube.transform.position = transform.position;
            counter = 0;
        }
    }

    public void StartTimeStop()
    {
        isStopped = true;
    }

    public void EndTimeStop()
    {
        isStopped = false;
    }
}
