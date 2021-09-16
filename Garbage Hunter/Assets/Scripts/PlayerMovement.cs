using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private const float NORMAL_FOV = 90f;
    private const float HOOKSHOT_FOV = 120f;

    [SerializeField] private Transform debugHitPointTransform;
    [SerializeField] private Transform hookshotTransform;

    public CharacterController controller;
    public float speed = 15f;
    private float velocity;
    private Vector3 characterVelocityMomentum;
    public float gravity = -75f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistnace = 0.6f;
    public LayerMask groundMask;
    private bool isGrounded;
    public Camera playerCamera;
    private MouseLook cameraFOV;
    public int jumpLimit = 1;
    private Vector3 hookshotPosition;
    private float hookshotSize;

    private State state;
    private enum State
    {
        Normal,
        HookshotFly,
        HookshotThrown
    }
    // Start is called before the first frame update
    void Start()
    {
        state = State.Normal;
        hookshotTransform.gameObject.SetActive(false);
        cameraFOV = playerCamera.GetComponent<MouseLook>();
    }


    // Update is called once per frame
    void Update()
    {

        switch(state)
        {
            default:
            case State.Normal:
                playerMovement();
                HandleHookshotStart();
                break;
            case State.HookshotThrown:
                HandleHookshotThrow();
                playerMovement();
                break;
            case State.HookshotFly:
                HandleHookshotMovement();
                break;


        }

    }

    private void playerMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 characterVelocity = transform.right * x * speed + transform.forward * z * speed;

        // Allows for double jumping
        if (isGrounded)
        {
            jumpLimit = 1;

        }
        if (Input.GetButtonDown("Jump") && jumpLimit != 0)
        {
            velocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpLimit--;
        }

        // Sprint Speed
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 20;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 15;
        }

        // Applies gravity
        velocity += gravity * Time.deltaTime;

        // Applies Y velocity to move vector
        characterVelocity.y = velocity;

        // Moves the character
        controller.Move(characterVelocity * Time.deltaTime);

        // Checks if player is on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistnace, groundMask);
        if (isGrounded && velocity < 0)
        {
            velocity = -1f;
        }

        // Apply Momentum
        characterVelocity += characterVelocityMomentum;

        // Moves character controller
        controller.Move(characterVelocity * Time.deltaTime);

        // Soften up the momentum
        if (characterVelocityMomentum.magnitude >= 0f)
        {
            float momentumDrag = 3f;
            characterVelocityMomentum -= characterVelocityMomentum * momentumDrag * Time.deltaTime;
            if ( characterVelocityMomentum.magnitude < .0f)
            {
                characterVelocityMomentum = Vector3.zero;
            }
        }
    }

    private void ResetGravity()
    {
        velocity = 0;
    }

    private void HandleHookshotStart()
    {
        if (InputDownHookshot())
        {
            if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit raycastHit)) {
                // if it hits something with a rigid body
                debugHitPointTransform.position = raycastHit.point;
                hookshotPosition = raycastHit.point;
                hookshotSize = 0f;
                hookshotTransform.gameObject.SetActive(true);
                hookshotTransform.localScale = Vector3.zero;
                state = State.HookshotThrown;
            }
        }
    }

    private void HandleHookshotThrow()
    {
        hookshotTransform.LookAt(hookshotPosition);
        float hookshotThrowSpeed = 200f;
        hookshotSize += hookshotThrowSpeed * Time.deltaTime;
        hookshotTransform.localScale = new Vector3(1, 1, hookshotSize);

        if(hookshotSize >= Vector3.Distance(transform.position, hookshotPosition))
        {
            state = State.HookshotFly;
            cameraFOV.setCameraFOV(HOOKSHOT_FOV);
        }
    }
    private void HandleHookshotMovement()
    {
        hookshotTransform.LookAt(hookshotPosition);

        Vector3 hookshotDirection = (hookshotPosition - transform.position).normalized;

        float hookshotSpeedMin = 50f;
        float hookshotSpeedMax = 80f;
        float hookshotSpeed = Mathf.Clamp(Vector3.Distance(transform.position, hookshotPosition), hookshotSpeedMin, hookshotSpeedMax);
        float hookshotSpeedMultiplier = 2f;
        // Moes the character controller
        controller.Move(hookshotDirection * hookshotSpeed * hookshotSpeedMultiplier * Time.deltaTime);

        float reachedHookshotPositionDistance = 1f;

        if (Vector3.Distance(transform.position, hookshotPosition) < reachedHookshotPositionDistance)
        {
            // If the hook shot reached its position
            StopHookshot();
        }

        if (InputDownHookshot())
        {
            //Cancel Hookshot
            StopHookshot();

        }

        if (InputJumpHookshot())
        {
            //Cancel Hookshot via jumping
            float momentumSpeed = 4f;
            characterVelocityMomentum = hookshotDirection * hookshotSpeed * momentumSpeed;

            float jumpSpeed = 40f;
            characterVelocityMomentum += Vector3.up * jumpSpeed;
            StopHookshot();

        }
    }

    private void StopHookshot()
    {
        state = State.Normal;
        ResetGravity();
        hookshotTransform.gameObject.SetActive(false);
        cameraFOV.setCameraFOV(NORMAL_FOV);
    }

    private bool InputDownHookshot()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    private bool InputJumpHookshot()
    {
        return Input.GetButtonDown("Jump");
    }
}
