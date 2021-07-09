using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 12f;
    [SerializeField] private float jumpImpulsion = 5;
    [SerializeField] private float maxfallDistance = 5;

    [Header("References")]
    [SerializeField] private new Camera camera = null;
    [SerializeField] private PlayerMovement playerMovement = null;
    [SerializeField] private MouseLook mouseLook = null;

    [Header("Pickup")]
    [SerializeField] private float pickupDistance = 3;
    [SerializeField] private LayerMask pickupMask;
    [SerializeField] private Transform hand = null;

    [Header("Interact")]
    [SerializeField] private float interactDistance = 3;
    [SerializeField] private LayerMask interactMask;

    [Header("Crouching References")]
    [SerializeField] private CapsuleCollider standingCollider = null;
    [SerializeField] private CapsuleCollider crouchingCollider = null;
    [SerializeField] private Transform standingCameraPosition = null;
    [SerializeField] private Transform crouchingCameraPosition = null;

    [Header("UIReferences")]
    [SerializeField] private GameObject investigationPanel = null;

    private TimeManager timeManager;

    private GameObject pickup = null;

    private bool isCrouched = false;

    private IInteractable interactableInRange = null;

    // Start is called before the first frame update
    void Start()
    {
        timeManager = FindObjectOfType<TimeManager>();
        playerMovement.OnCharacterLanded += PlayerLanded;
    }

    // Update is called once per frame
    void Update()
    {
        // Nouvelle façon de déplacer le joueur à partir de la vidéo de Brackey
        // Le code de la rotation du joueur par rapport à la souris a été déplacé vers MouseLook

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 move = (transform.right * x + transform.forward * z).normalized;

        playerMovement.Move(move * speed / Time.timeScale);

        CheckInteractable();
     
        if (Input.GetButtonDown("Jump"))
        {
            playerMovement.Jump(jumpImpulsion);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            if (isCrouched)
            {
                camera.transform.position = standingCameraPosition.position;
                standingCollider.enabled = true;
                crouchingCollider.enabled = false;
                isCrouched = false;
            }
            else
            {
                camera.transform.position = crouchingCameraPosition.position;
                standingCollider.enabled = false;
                crouchingCollider.enabled = true;
                isCrouched = true;
            }
        }

        // Utilise l'item porté
        if (Input.GetButtonDown("Interact"))
        {
            if(interactableInRange != null)
            {
                interactableInRange.Interact(pickup, this);
            }
            else if (pickup != null)
            {
                if (pickup.GetComponent<TimeChanger>() != null)
                {
                    pickup.GetComponent<TimeChanger>().ChangeTime();
                    Destroy(pickup);
                    pickup = null;
                }
            }
        }

        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1f))
        {
            //Change buttons to be interactables
            if (hit.collider.tag == "Button" )
            {
                if(timeManager.multiplier !=0)
                {
                    if (Input.GetKeyDown("e") == true)
                    {
                        hit.collider.GetComponent<Button>().clicked = true;
                    }
                }

            }

        }

        if(pickup == null)
        {
            if(Input.GetButtonDown("Pickup"))
            {
                TryPickupItem();
            }
        }


        //Lache l'item
        if (pickup != null)
        {
            if (Input.GetButtonDown("Drop"))
            {
                pickup.transform.parent = null;
                pickup = null;
            }
        }

        if (Input.GetButtonDown("OpenInvestigation"))
        {
            OpenInvestigationPressed();
        }

        if(Input.GetKeyDown(KeyCode.Equals))
        {
            timeManager.RestartLoop();
        }
    }

    public void ReleaseHeldObject()
    {
        pickup = null;
    }

    private void OpenInvestigationPressed()
    {
        if (investigationPanel)
        {
            investigationPanel.SetActive(!investigationPanel.activeInHierarchy);
            mouseLook.ChangeCursorLockMode(!investigationPanel.activeInHierarchy);
        }
    }

    private void PlayerLanded(float fallenDistance)
    {
        if(fallenDistance >= maxfallDistance)
        {
            Die();
        }
    }

    private void TryPickupItem()
    {
        RaycastHit hit;

        if (Physics.Raycast(new Ray(camera.transform.position, camera.transform.forward), out hit, pickupDistance, pickupMask))
        {
            pickup = hit.collider.gameObject;
            pickup.transform.parent = hand;
            pickup.transform.position = hand.position;
        } 
    }

    private void CheckInteractable()
    {
        RaycastHit hit;
        
        if(Physics.Raycast(new Ray(camera.transform.position, camera.transform.forward), out hit, interactDistance, interactMask))
        {
            IInteractable foundInteractable = hit.collider.GetComponent<IInteractable>();
            if(interactableInRange != foundInteractable)
            {
                if (interactableInRange != null)
                {
                    interactableInRange.PlayerHoverEnd();
                }

                interactableInRange = foundInteractable;
                interactableInRange.PlayerHoverStart();
            }
        }
        else 
        {
            if (interactableInRange != null)
            {
                interactableInRange.PlayerHoverEnd();
            }
            interactableInRange = null;
        }
    }

    private void Die()
    {
        timeManager.RestartLoop();
    }
}
