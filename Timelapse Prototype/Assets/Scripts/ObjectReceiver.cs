using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectReceiver : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform receivedObjectMarker = null;
    [SerializeField] private MeshRenderer interactMesh = null;

    public UnityEvent OnObjectReceived;

    private GameObject held = null;

    public void DepositObject(GameObject obj)
    {
        
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
        if(pickup && !held)
        {
            held = pickup;

            pickup.transform.parent = receivedObjectMarker;
            pickup.transform.position = receivedObjectMarker.position;
            pickup.transform.rotation = receivedObjectMarker.rotation;

            player.ReleaseHeldObject();

            OnObjectReceived?.Invoke();
        }
        
    }
}
