using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallrunning : MonoBehaviour
{
    [Header("Wallrunning")]
    public LayerMask whatIsWall;
    public LayerMask whatIsGround;
    public float wallRunForce;
    public float wallJumpUpForce;
    public float wallJumpSideForce;
    public float wallClimbSpeed;
    public float maxWallRunTime;
    private float wallRunTimer;

    [Header("Input")]
    public KeyCode upwardsWallrunKey = KeyCode.LeftShift;
    public KeyCode downwardsWallrunKey = KeyCode.LeftControl;
    public KeyCode wallJumpKey = KeyCode.Space;
    private bool upwardsWallrun;
    private bool downwardsWallrun;
    private float horizontalInput;
    private float verticalInput;

    [Header("Exit Wallrun")]
    private bool exitingWall;
    public float exitWallTime;
    private float exitWallTimer;

    [Header("Gravity")]
    public bool useGravity;
    public float gravitycounterForce;

    [Header("Wall Detection")]
    public float wallCheckDistance;
    public float minJumpHeight;
    private RaycastHit leftWallHit;
    private RaycastHit rightWallHit;
    private bool wallLeft;
    private bool wallRight;

    [Header("References")]
    public Transform orientaiton;
    public CameraController cam;
    private PlayerMovement movement;
    private Rigidbody rb;
    public GameManager manager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        movement = GetComponent<PlayerMovement>();     
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();       
    }

    private void Update()
    {
        if (!manager.paused)
        {
            CheckForWall();
            StateMachine();
        }
    }
    private void FixedUpdate()
    {
        if(movement.wallrunning)
        {
            WallRunMovement();
        }
    }
    private void CheckForWall()
    {
        wallRight = Physics.Raycast(transform.position, orientaiton.right, out rightWallHit, wallCheckDistance, whatIsWall);
        wallLeft = Physics.Raycast(transform.position, -orientaiton.right, out leftWallHit, wallCheckDistance, whatIsWall);

    }
    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround);
    }

    private void StateMachine()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        upwardsWallrun = Input.GetKey(upwardsWallrunKey);
        downwardsWallrun = Input.GetKey(downwardsWallrunKey);

        //Wallrunning State
        if ((wallLeft || wallRight) && verticalInput > 0 && AboveGround() && !exitingWall)
        {
            if(!movement.wallrunning)
            {
                StartWallRun();               
            }
            if (Input.GetKeyDown(wallJumpKey))
            {
                WallJump();
            }
            if (wallRunTimer > 0)
            {
                wallRunTimer -= Time.deltaTime;
            }

            if (wallRunTimer <= 0 && movement.wallrunning)
            {
                exitingWall = true;
                exitWallTimer = exitWallTime;
            }
        } 
        //if user tries to exit wall
        else if (exitingWall)
        {
            if(movement.wallrunning)
            {
                StopWallRun();             
            }
            if (exitWallTimer > 0)
            {
                exitWallTimer -= Time.deltaTime;
            }

            if (exitWallTimer <= 0)
            {
                exitingWall = false;
            }
        }
        //if wallrunning and no wall, no wallrun
        else
        {
            if(movement.wallrunning)
            {
                StopWallRun();
            }
        }
    }

    private void StartWallRun()
    {
        movement.wallrunning = true;
        wallRunTimer = maxWallRunTime;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        cam.ChangeFov(80f);
        if (wallRight) cam.Tilt(5f);
        if (wallLeft) cam.Tilt(-5f);
    }

    private void WallRunMovement()
    {
        rb.useGravity = useGravity;
        

        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;
        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        if((orientaiton.forward - wallForward).magnitude > (orientaiton.forward - -wallForward).magnitude)
        {
            wallForward = -wallForward;
        }

        rb.AddForce(wallForward * wallRunForce, ForceMode.Force);

        //climb up and down
        if(upwardsWallrun)
        {
            rb.velocity = new Vector3(rb.velocity.x, wallClimbSpeed, rb.velocity.z);
        }
        if (downwardsWallrun)
        {
            rb.velocity = new Vector3(rb.velocity.x, -wallClimbSpeed, rb.velocity.z);
        }

        //push player towards wall if player is not pushing away
        if (!(wallLeft && horizontalInput > 0) && !(wallRight && horizontalInput < 0))
        {
            rb.AddForce(-wallNormal * 100f, ForceMode.Force);
        }

        if(useGravity)
        {
            rb.AddForce(transform.up * gravitycounterForce, ForceMode.Force);
        }

    }

    private void StopWallRun()
    {
        movement.wallrunning = false;
        if (!useGravity)
        {
            rb.useGravity = true;
        }

        cam.ChangeFov(60f);
        cam.Tilt(0f);
    }

    private void WallJump()
    {
        exitingWall = true;
        exitWallTimer = exitWallTime;

        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;

        Vector3 wallJumpForce = transform.up * wallJumpUpForce + wallNormal * wallJumpSideForce;

        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(wallJumpForce, ForceMode.Impulse);
    }

}
