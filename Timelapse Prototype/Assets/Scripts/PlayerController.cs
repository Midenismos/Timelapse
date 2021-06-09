using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody body = null;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move (Vector3 velocity)
    {
        float verticalVelocity = body.velocity.y;
        Vector3 bodyVelocity = velocity;
        bodyVelocity.y += verticalVelocity;
        body.velocity = bodyVelocity;
    }

    public void Jump(float impulsion)
    {
        body.velocity += new Vector3(0, impulsion, 0);
    }
}
