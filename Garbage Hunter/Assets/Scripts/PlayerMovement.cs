using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public int PlayerScore = 0;

   private const float NORMAL_FOV = 90f;
   private const float HOOKSHOT_FOV = 120f;
    //private bool playerlock = false;

    private int lowestfloor = -40;

    [SerializeField] private Transform debugHitPointTransform;
    [SerializeField] private Transform hookshotTransform;

    public CharacterController controller;
    public float speed = 12f;
    private float velocity;
    private Vector3 characterVelocityMomentum;
    public float gravity = -75f;
    public float jumpHeight = 3f;
    
    // Ground checker
    public float groundDistnace = 0.6f;
    public LayerMask groundMask;
    private bool isGrounded;
    public Transform groundCheck;

    // Camera 
    public Camera playerCamera;
    private MouseLook cameraFOV;

    // Allows for double jump
    public int jumpLimit = 1;

    // hook shot
    private Vector3 hookshotPosition;
    private float hookshotSize;

    // Dashing
    public float dashSpeed;
    public float dashTime;
    public float dashCooldown = 2f; // Cooldown of dash
    private float dashNextDash = 0f; // Cooldown until the NEXT dash

    private State oldstate;

    private State state;
    private enum State
    {
        Normal,
        HookshotFly,
        HookshotThrown,
        Locked
    }

    //Inventory
    public ContainerScript inventory;

    public float inv_Space; //Max Space  (trash+plastic+glass+paper <= invSpace)
    
    public float trash=0;
    public float plastic=0;
    public float glass=0;
    public float paper=0;


    // PowerBank

    public float p1 = 0; // speed (1 * n)
    public float p2 = 0; //jump height? (? * n)
    public float p3 = 0; //increase jump count?? (1 +n) 
    public float p4 = 0; //inventory space?? (+5*n)?
    public float p5 = 0; //reduce dash cooldown?? (



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
            case State.Locked:
                break;
        }
        inventory.setValue((int)(trash + plastic + glass + paper));
    }

    void LateUpdate()
    {
        if (UnderMap())
        {
            controller.transform.position = new Vector3(37,10,-43);
        }
    }

    public void sortlock()
    {
        oldstate = state;
        state = State.Locked;
        cameraFOV.CursorUnlock();
    }

    public void sortunlock()
    {
        state = oldstate;
        cameraFOV.CursorLock();
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
            FindObjectOfType<AudioManager>().Play("Jump");
            velocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpLimit--;
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

        // Allows the character to dash
        if (Time.time > dashNextDash)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartCoroutine(Dash());
                Debug.Log("Dashed... Cooldown started... hopefully...");
                dashNextDash = Time.time + dashCooldown;
            }
        }

    }

    IEnumerator Dash()
    {
        FindObjectOfType<AudioManager>().Play("Dash");

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 characterVelocity = transform.right * x * speed + transform.forward * z * speed;
        float startTime = Time.time;
        while(Time.time < startTime + dashTime)
        {
            controller.Move(characterVelocity * dashSpeed * Time.deltaTime);
            jumpLimit = 1;
            yield return null;
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
        float hookshotThrowSpeed = 400f;
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
            FindObjectOfType<AudioManager>().Play("Jump");
            //Cancel Hookshot via jumping
            float momentumSpeed = 4f;
            characterVelocityMomentum = hookshotDirection * hookshotSpeed * momentumSpeed;

            float jumpSpeed = 40f;
            characterVelocityMomentum += Vector3.up * jumpSpeed;

            dashNextDash = 0;
            StopHookshot();

        }
    }

    private void StopHookshot()
    {
        jumpLimit = 1;
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

    private bool UnderMap()
    {
        return (controller.transform.position.y < lowestfloor);
    }

    //getters

    public float getTrash()
    {
        return trash;
    }

    public float getPlastic()
    {
        return plastic;
    }

    public float getPaper()
    {
        return paper;
    }

    public float getGlass()
    {
        return glass;
    }

    public float getInvSpace()
    {
        return inv_Space;
    }

    //setters

    public void setInvSpace(float a)
    {
        inv_Space = a;
    }

    public void addTrash()
    {
        trash += 1;
    }

    public void addPlastic()
    {
        plastic +=1;
    }
    public void addGlass()
    {
        glass += 1;
    }
    public void addPaper()
    {
        paper += 1;
    }

    public void removeTrash(int a)
    {
        trash -= a;
    }

    public void removePaper(int a)
    {
        paper -= a;
    }

    public void removePlastic(int a)
    {
        plastic -= a;
    }

    public void removeGlass(int a)
    {
        glass -= a;
    }

    //POINT
    public void addPoint(int a)
    {
        PlayerScore += a;
    }

    public int getPoint()
    {
        return PlayerScore;
    }

    //counter reset

    public bool resetInv()
    {   
        bool output =false;
        if (trash != 0)
        {
            trash = 0;
            output = true;
        }
        if (paper != 0)
        {
            paper = 0;
            output = true;
        }
        if (plastic != 0)
        {
            plastic = 0;
            output = true;
        }
        if (glass != 0)
        {
            glass = 0;
            output = true;
        }

        return output;
    }

}
