using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourPlayer : MonoBehaviour
{
    public Rigidbody rb;
    public Camera cam;
    
    private float speed = 100;
    private float VerticalMove;
    private float HorizontalMove;
    private float velocityX;
    private float velocityY =0;
    private float velocityZ;

    private Vector3 moveDirection;

    private float jumpPower = 5f;
    private float gravity = 9.81f ;

    private float rotationY;
    private float rotationX;
    private float minimumX = -60f;
    private float maximumX = 60f;


    private RaycastHit hit;

    public GameObject pickup = null;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Gère la rotation du joueur avec la souris
        rotationY += Input.GetAxis("Mouse X");
        rotationX += Input.GetAxis("Mouse Y");

        rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);

        cam.transform.localEulerAngles = new Vector3(-rotationX,0, 0);
        transform.localEulerAngles = new Vector3(0, rotationY, 0);

        // Gère les inputs de déplacements du joueur
        HorizontalMove = Input.GetAxis("Horizontal");
        VerticalMove = Input.GetAxis("Vertical");

        velocityX = HorizontalMove;
        velocityZ = VerticalMove;

        //Gère la gravité et le saut
        if (Input.GetKey(KeyCode.Space) == true && Physics.Raycast(transform.position, new Vector3(0, -1), out hit, 1f) == true)
        {
            velocityY = Mathf.Sqrt(jumpPower * 2.0f * gravity);
        }
        else if (Physics.Raycast(transform.position, new Vector3(0, -1), out hit, 1f) == true)
        {
            velocityY = 0;
        }
        else
        {
            velocityY -= gravity * Time.fixedDeltaTime;
        }

        moveDirection = (transform.right * velocityX + transform.forward * velocityZ).normalized;

        // Utilise l'item porté
        if (Input.GetKeyDown("e") == true)
        {
            pickup.GetComponent<TimeChanger>().changeTime();
            Destroy(pickup);
            pickup = null;
        }
    }

    void FixedUpdate()
    {
        Move();

        //Colle l'item porté par le joueur près de lui
        if (pickup != null)
        {
            pickup.transform.parent = gameObject.transform;
        }
    }

    //Déplace le joueur
    public void Move()
    {
        rb.velocity = moveDirection * speed * Time.deltaTime;
        rb.velocity += new Vector3 (rb.velocity.x, velocityY, rb.velocity.z);
    }

    //Permet au joueur de prendre des items
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pickup")
        {
            if (pickup == null)
            {
                if (Input.GetKey("a"))
                {
                    pickup = other.gameObject;
                }
            }
        }
    }
}
