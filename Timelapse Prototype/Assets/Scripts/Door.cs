using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : Rewindable, ITimeStoppable
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private string cardName = null;

    public UnityEvent OnDoorOpened = null;

    private bool isOpen = false;


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

    public override void StartRewind(float timestamp)
    {
        base.StartRewind(timestamp);
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

    // ouvre et ferme la porte
    public void OpenDoor()
    {
        //animator.SetBool("character_nearby", true);
        animator.SetTrigger("Open");
        isOpen = true;
        OnDoorOpened?.Invoke();
    }
    public void CloseDoor()
    {
        animator.SetBool("character_nearby", false);
        isOpen = false;
    }

    // vérifie que le joueur possède la bonne carte d'accès
    public void ScanCard()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (isOpen == false)
        {
            if (player.pickup != null)
            {
                if (player.pickup.name == cardName)
                {
                    if (player.pickup.GetComponent<Card>().isBroken == false)
                    {

                        OpenDoor();
                        FindObjectOfType<SoundManager>().Play("AccessGranted");
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
}
