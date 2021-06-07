using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourPlayer : MonoBehaviour
{


    public GameObject pickup = null;
    public new Camera camera = null;

    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [SerializeField] private float pickupDistance = 3;
    [SerializeField] private LayerMask pickupMask;

    bool isGrounded;

    private GameObject TimeManager;

    // Start is called before the first frame update
    void Start()
    {
        TimeManager = GameObject.Find("TimeManager");
    }

    // Update is called once per frame
    void Update()
    {
        // Nouvelle façon de déplacer le joueur à partir de la vidéo de Brackey
        // Le code de la rotation du joueur par rapport à la souris a été déplacé vers MouseLook
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump")&& isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
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
                if(TimeManager.GetComponent<TimeManager>().multiplier !=0)
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
    }

    void FixedUpdate()
    {
        //Colle l'item porté par le joueur près de lui
        if (pickup != null)
        {
            pickup.transform.parent = gameObject.transform;
        }
    }


    //Permet au joueur de prendre des items
    //public void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.tag == "Pickup")
    //    {
    //        if (pickup == null)
    //        {
    //            if (Input.GetKeyDown("a"))
    //            {
    //                pickup = other.gameObject;
    //            }
    //        }
    //    }
    //}

    private void TryPickupItem()
    {
        RaycastHit hit;

        if (Physics.Raycast(new Ray(camera.transform.position, camera.transform.forward), out hit, pickupDistance, pickupMask))
        {
            pickup = hit.collider.gameObject;
        } 
    }
}
