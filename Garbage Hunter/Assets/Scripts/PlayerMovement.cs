using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 15f;
    private Vector3 velocity;
    public float gravity = -75f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistnace = 0.6f;
    public LayerMask groundMask;
    private bool isGrounded;

    public int jumpLimit = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");

        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (isGrounded)
        {
            jumpLimit = 1;

        }
        if (Input.GetButtonDown("Jump") && jumpLimit != 0)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpLimit--;
        }


        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistnace, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 20;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 15;
        }

    }
}
