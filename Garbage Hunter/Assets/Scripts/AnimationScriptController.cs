using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScriptController : MonoBehaviour
{


    Animator animator;
    // Ground checker
    public float groundDistnace = 0.6f;
    public LayerMask groundMask;
    private bool isGrounded;
    public Transform groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        bool forwardPressed = Input.GetKey("w");
        bool jumpPressed = Input.GetKey("space");
        bool backwardPressed = Input.GetKey("s");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");

        bool isRunning = animator.GetBool("isRunning");
        bool isRunningBack = animator.GetBool("isRunningBack");
        bool isJumping = animator.GetBool("isJumping");
        bool isStrafingLeft = animator.GetBool("isStrafeLeft");
        bool isStrafingRight = animator.GetBool("isStrafeRight");
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistnace, groundMask);

        // Running forward
        if (!isRunning && forwardPressed)
        {
            animator.SetBool("isRunning", true);
        }

        if(isRunning && !forwardPressed)
        {
            animator.SetBool("isRunning", false);
        }
    
        // Running Backward
        if(!isRunningBack && backwardPressed)
        {
            animator.SetBool("isRunningBack", true);
        }
        if(isRunningBack && !backwardPressed)
        {
            animator.SetBool("isRunningBack", false);
        }

        // Jumping
        if (jumpPressed && !isJumping)
        {
            animator.SetBool("isJumping", true);
        }
        if(isGrounded)
        {
            animator.SetBool("isJumping", false);
        }

         // Strafing left
        if (leftPressed && !isStrafingLeft)
        {
            animator.SetBool("isStrafeLeft", true);
        }
        if (!leftPressed && isStrafingLeft)
        {
            animator.SetBool("isStrafeLeft", false);
        }

        // Strafing right
        if (rightPressed && !isStrafingRight)
        {
            animator.SetBool("isStrafeRight", true);
        }
        if (!rightPressed && isStrafingRight)
        {
            animator.SetBool("isStrafeRight", false);
        }
    }
}
