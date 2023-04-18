using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform orientation;
    public Climbing climbingScript;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Movement Booleans")]
    public bool sliding = false;
    public bool wallrunning = false;
    public bool crouching = false;
    public bool climbing = false;

    [Header("Movement Variables")]
    public float walkSpeed;
    public float sprintSpeed;
    private float moveSpeed;
    public float slideSpeed;
    public float climbSpeed;
    public float wallrunSpeed;
    public float groundDrag;

    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;

    [Header("Jumping Variables")]
    public float jumpPower;
    public float jumpCooldown;
    public float airMultiplier;
    public float jumpLeniency;
    public int numberOfJumps;
    bool canJump = true;

    [Header("Crouching Variables")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatisGround;

    [Header("Slope Check")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool slopeJump;

    [Header("Debug Variables, Do Not Change")]
    public int jumpCount = 0;
    public bool grounded;


    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;

    public enum MovementState
    {
        walking,
        sprinting,
        wallrunning,
        climbing,
        crouching,
        sliding,
        air
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + jumpLeniency, whatisGround);

        GetInput();
        LimitSpeed();
        StateHandler();

        if(grounded)
        {
            jumpCount = 0;
            rb.drag = groundDrag;
        } 
        else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate() //need to use fixed update for physics based calculations 
    {
        MovePlayer();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && canJump && (grounded || jumpCount < numberOfJumps))
        {
            Debug.Log("Jump key pressed");

            canJump = false;

            Jump();
    
            Invoke(nameof(ResetJump), jumpCooldown); //so the player cannot jump constantly
        }

        if(Input.GetKeyDown(crouchKey))
        {
            crouching = true;
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
        if(Input.GetKeyUp(crouchKey))
        {
            crouching = false;
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    private void StateHandler()
    {
        if(climbing)
        {
            state = MovementState.climbing;
            desiredMoveSpeed = climbSpeed;
        }
        else if(wallrunning)
        {
            state = MovementState.wallrunning;
            desiredMoveSpeed = wallrunSpeed;
        }
        else if(sliding)
        {
            state = MovementState.sliding;
            
            if (OnSlope() && rb.velocity.y < 0.1f) {
                desiredMoveSpeed = slideSpeed;
            } 
            else
            {
                desiredMoveSpeed = sprintSpeed;
            }
        }
        else if(crouching)
        {
            state = MovementState.crouching;
            desiredMoveSpeed = crouchSpeed;
        }
        else if(grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            desiredMoveSpeed = sprintSpeed;
        }
        else if (grounded)
        {
            state = MovementState.walking;
            desiredMoveSpeed = walkSpeed;
        } 
        else
        {
            state = MovementState.air;
        }

        if(Mathf.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > 4f && moveSpeed != 0) //instantly change speed for short distances such as walk -> sprint
        {
            StopAllCoroutines();
            StartCoroutine(SmoothlyLerpMoveSpeed());
        } 
        else
        {
            moveSpeed = desiredMoveSpeed;
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;
    }

    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);
            time += Time.deltaTime;
            yield return null;
        }

        moveSpeed = desiredMoveSpeed;
    }
    private void MovePlayer()
    {
        if (climbingScript.exitingWall) {
            return; 
        }
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(OnSlope() && !slopeJump)
        {
            rb.AddForce(GetSlopeMoveDirection(moveDirection) * moveSpeed * 20f, ForceMode.Force); 

            if(rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force); //force since force is applied constantly
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force); //force since force is applied constantly
        }

        if (!wallrunning)
        {
            rb.useGravity = !OnSlope();
        }
    }

    private void LimitSpeed()
    {
        if (OnSlope() && !slopeJump)
        {
            if (rb.velocity.magnitude > moveSpeed)
            {
                rb.velocity = rb.velocity.normalized * moveSpeed;
            }
        }
        else
        {
            Vector3 velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (velocity.magnitude > moveSpeed)
            {
                Vector3 limitedSpeed = velocity.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedSpeed.x, rb.velocity.y, limitedSpeed.z);
            }
        }
    }

    private void Jump()
    {
        slopeJump = true;

        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(transform.up * jumpPower, ForceMode.Impulse); //impulse since force is only applied once
    }

    private void ResetJump()
    {
        slopeJump = false;
        jumpCount++;
        canJump = true;   
    }

    public bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }

}
