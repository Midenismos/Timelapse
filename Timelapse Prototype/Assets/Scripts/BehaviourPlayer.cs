using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourPlayer : MonoBehaviour
{


    [Header("Movement Settings")]
    [SerializeField] private float speed = 12f;
    [SerializeField] private float jumpImpulsion = 5;
    [SerializeField] private float maxfallDistance = 5;

    [Header("References")]
    [SerializeField] private new Camera camera = null;
    [SerializeField] private PlayerController playerController = null;

    [Header("Pickup")]
    [SerializeField] private float pickupDistance = 3;
    [SerializeField] private LayerMask pickupMask;
    [SerializeField] private Transform hand = null;

    [Header("Crouching References")]
    [SerializeField] private CapsuleCollider standingCollider = null;
    [SerializeField] private CapsuleCollider crouchingCollider = null;
    [SerializeField] private Transform standingCameraPosition = null;
    [SerializeField] private Transform crouchingCameraPosition = null;


    private TimeManager timeManager;

    private GameObject pickup = null;

    private bool isCrouched = false;

    // Start is called before the first frame update
    void Start()
    {
        timeManager = FindObjectOfType<TimeManager>();
        playerController.OnCharacterLanded += PlayerLanded;
    }

    // Update is called once per frame
    void Update()
    {
        // Nouvelle façon de déplacer le joueur à partir de la vidéo de Brackey
        // Le code de la rotation du joueur par rapport à la souris a été déplacé vers MouseLook

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 move = (transform.right * x + transform.forward * z).normalized;

        playerController.Move(move * speed / Time.timeScale);
     
        if (Input.GetButtonDown("Jump"))
        {
            playerController.Jump(jumpImpulsion);
        }


        // Utilise l'item porté
        if (Input.GetKeyDown("e") == true)
        {
            if (pickup != null)
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
            if(Input.GetKeyDown("a"))
            {
                TryPickupItem();
            }
        }


        //Lache l'item
        if (pickup != null)
        {
            if (Input.GetKeyDown("r"))
            {
                pickup.transform.parent = null;
                pickup = null;
            }
        }

        if(Input.GetKeyDown("c"))
        {
            if(isCrouched)
            {
                camera.transform.position = standingCameraPosition.position;
                standingCollider.enabled = true;
                crouchingCollider.enabled = false;
                isCrouched = false;
            } else
            {
                camera.transform.position = crouchingCameraPosition.position;
                standingCollider.enabled = false;
                crouchingCollider.enabled = true;
                isCrouched = true;
            }
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

    private void Die()
    {
        timeManager.RestartLoop();
    }
}
