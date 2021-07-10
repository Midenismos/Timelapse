using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Rewindable, ITimeStoppable
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private string cardName = null;
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

    // ouvre et ferme la porte
    public void OpenDoor()
    {
        animator.SetBool("character_nearby", true);
        isOpen = true;
    }
    public void CloseDoor()
    {
        animator.SetBool("character_nearby", false);
        isOpen = false;
    }

    // vérifie que le joueur possède la bonne carte d'accès
    public void ScanCard(GameObject Player)
    {
        if (Player.GetComponent<PlayerController>().pickup != null)
        {
            if (Player.GetComponent<PlayerController>().pickup.name == cardName)
            {
                if (Player.GetComponent<PlayerController>().pickup.GetComponent<Card>().isBroken == false)
                {
                    if (isOpen == false)
                    {
                        OpenDoor();
                        FindObjectOfType<SoundManager>().Play("AccessGranted");
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
        else
        {
            FindObjectOfType<SoundManager>().Play("Fail");
        }
    }
}
