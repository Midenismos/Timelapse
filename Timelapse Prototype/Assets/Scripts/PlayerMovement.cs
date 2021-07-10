using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody body = null;

    [SerializeField] private Transform groundCheck = null;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    public event Action<float> OnCharacterLanded;
    public bool isGrounded = true;
    private float fallenDistance = 0;





    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        bool oldIsGrounded = isGrounded;
        //TOChange
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && !oldIsGrounded)
        {
            OnCharacterLanded?.Invoke(fallenDistance);
            fallenDistance = 0;
        } else if(!isGrounded && body.velocity.y < 0)
        {
            fallenDistance -= body.velocity.y * Time.unscaledDeltaTime;
        }
        if (isGrounded && body.velocity.y <-8)
        {
            FindObjectOfType<SoundManager>().ChangePitch("Jump");
            FindObjectOfType<SoundManager>().Play("Jump");
        }
    }

    public void Move(Vector3 velocity)
    {
        float verticalVelocity = body.velocity.y;
        Vector3 bodyVelocity = velocity;
        bodyVelocity.y += verticalVelocity;
        body.velocity = bodyVelocity;
    }

    public void Jump(float impulsion)
    {
        if(isGrounded)
        {
            body.velocity += new Vector3(0, impulsion, 0);
        }
    }
}
