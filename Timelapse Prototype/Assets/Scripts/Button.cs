using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour, IInteractable, ITimeStoppable
{
    [SerializeField] private MeshRenderer interactMesh = null;
    public bool clicked = false;

    public float timerSinceClicked = 0.0f;

    private TimeManager timeManager;
    public float multiplier = 1f;

    public Material buttonActivatedMaterial = null;
    public Material buttonDeactivatedMaterial = null;

    public GameObject rewindedActionObject = null;
    public string rewindedFunction;

    //private bool isTimeStopped
    // Start is called before the first frame update
    void Start()
    {
        timeManager = FindObjectOfType<TimeManager>();
        if (timeManager)
            timeManager.RegisterTimeStoppable(this);
    }

    // Update is called once per frame
    void Update()
    {

        if (clicked)
        {
            timerSinceClicked += Time.deltaTime;
        }
        if (timerSinceClicked < 0)
        {
            clicked = false;
            rewindedActionObject.SendMessage(rewindedFunction);
            timerSinceClicked = 0;

            GetComponent<MeshRenderer>().material = buttonDeactivatedMaterial;
        }

    }

    public void PlayerHoverStart()
    {
        interactMesh.enabled = true;
    }

    public void PlayerHoverEnd()
    {
        interactMesh.enabled = false;
    }

    public void Interact(GameObject pickup, PlayerController player)
    {
        FindObjectOfType<SoundManager>().ChangePitch("Button");
        FindObjectOfType<SoundManager>().Play("Button");
        clicked = true;
        GetComponent<MeshRenderer>().material = buttonActivatedMaterial;

    }

    public void StartTimeStop()
    {
        //TODO
    }

    public void EndTimeStop()
    {
        //TODO
    }

    private void OnDestroy()
    {
        if (timeManager)
            timeManager.UnRegisterTimeStoppable(this);
    }
}
