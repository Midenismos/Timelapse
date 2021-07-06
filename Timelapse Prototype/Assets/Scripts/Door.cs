using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Rewindable, ITimeStoppable
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private string cardName = null;


    private void Start()
    {
        TimeManager timeManager = FindObjectOfType<TimeManager>();
        if (timeManager)
            timeManager.RegisterTimeStoppable(this);
    }
    public void StartTimeStop()
    {
        animator.enabled = false;
    }

    public void EndTimeStop()
    {
        animator.enabled = true;
    }

    public override void StartRewind()
    {
        base.StartRewind();
    }

    public override void EndRewind()
    {
        base.EndRewind();
    }

    private void OnDestroy()
    {
        TimeManager timeManager = FindObjectOfType<TimeManager>();
        if (timeManager)
            timeManager.RegisterTimeStoppable(this);
    }

    public void OpenDoor()
    {
        animator.SetBool("character_nearby", true);
    }
    public void CloseDoor()
    {
        animator.SetBool("character_nearby", false);
    }

    public void ScanCard(GameObject Player)
    {
        Debug.Log("Searching");
        if (Player.GetComponent<PlayerController>().pickup != null)
        {
            if (Player.GetComponent<PlayerController>().pickup.name == cardName)
            {
                if (Player.GetComponent<PlayerController>().pickup.GetComponent<Card>().isBroken == false)
                {
                    FindObjectOfType<SoundManager>().Play("AccessGranted");
                    OpenDoor();
                }
                else
                {
                    FindObjectOfType<SoundManager>().Play("Fail");
                }
            }
            else
            {
                FindObjectOfType<SoundManager>().Play("Fail");
            }
        }
        else
        {
            FindObjectOfType<SoundManager>().Play("Fail");
        }
    }
}
