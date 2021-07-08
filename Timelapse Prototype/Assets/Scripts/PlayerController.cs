using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 12f;
    [SerializeField] private float jumpImpulsion = 5;
    [SerializeField] private float maxfallDistance = 5;
    [SerializeField] private bool isMovingSound = false;

    [Header("References")]
    [SerializeField] private new Camera camera = null;
    [SerializeField] private PlayerMovement playerMovement = null;

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


    private TimeManager timeManager;

    public GameObject pickup = null;

    private bool isCrouched = false;

    private IInteractable interactableInRange = null;

    private bool isDead = false;

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

        if (isDead == false)
        {
            playerMovement.Move(move * speed / Time.timeScale);
        }

        if (gameObject.GetComponent<Rigidbody>().velocity.magnitude > 0.01f && isMovingSound == false)
        {
            if (playerMovement.isGrounded == true)
            {
                FindObjectOfType<SoundManager>().Play("Walk");
            }
            else
            {
                FindObjectOfType<SoundManager>().Stop("Walk");
            }
            isMovingSound = true;
        }
        else if (gameObject.GetComponent<Rigidbody>().velocity.magnitude <= 0.01f && isMovingSound == true)
        {
            FindObjectOfType<SoundManager>().Stop("Walk");
            isMovingSound = false;
        }
        if (playerMovement.isGrounded == false)
        {
            FindObjectOfType<SoundManager>().Stop("Walk");
            isMovingSound = false;
        }

        CheckInteractable();

        if (isDead == false)
        {
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
                    if (pickup.GetComponent<FoodType>().Foodtype == "Eat")
                    {
                        FindObjectOfType<SoundManager>().ChangePitch("Eat");
                        FindObjectOfType<SoundManager>().Play("Eat");
                    }
                    else if (pickup.GetComponent<FoodType>().Foodtype == "Drink")
                    {
                        FindObjectOfType<SoundManager>().ChangePitch("Drink");
                        FindObjectOfType<SoundManager>().Play("Drink");
                    }
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
                        FindObjectOfType<SoundManager>().ChangePitch("Button");
                        FindObjectOfType<SoundManager>().Play("Button");
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

       
    }

    public void ReleaseHeldObject()
    {
        pickup = null;
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
    IEnumerator Death()
    {
        // Empêche le joueur de bouger afin de simuler une "animation de mort" pendant la durée du son de mort et relance la loop une fois le son terminé.
        isDead = true;
        yield return new WaitForSeconds(FindObjectOfType<SoundManager>().FindSound("Death").length);
        timeManager.RestartLoop();
        yield return null;
    }

    private void Die()
    {
        // Lance la couroutine de mort du joueur
        FindObjectOfType<SoundManager>().Play("Death");
        StartCoroutine("Death");
    }
}
