using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform orientation;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Movement Variables")]
    public float moveSpeed;
    public float maxSpeed;
    public float groundDrag;

    [Header("Jumping Variables")]
    public float jumpPower;
    public float jumpCooldown;
    public float airMultiplier;
    public float jumpLeniency;
    public int numberOfJumps;
    bool canJump = true;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatisGround;

    [Header("Debug Variables, Do Not Change")]
    public int jumpCount = 0;
    public bool grounded;


    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + jumpLeniency, whatisGround);

        GetInput();
        LimitSpeed();

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
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force); //force since force is applied constantly
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force); //force since force is applied constantly
        }
    }

    private void LimitSpeed()
    {
        Vector3 velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(velocity.magnitude > maxSpeed)
        {
            Vector3 limitedSpeed = velocity.normalized * maxSpeed;
            rb.velocity = new Vector3(limitedSpeed.x, rb.velocity.y, limitedSpeed.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(transform.up * jumpPower, ForceMode.Impulse); //impulse since force is only applied once
    }

    private void ResetJump()
    {
        jumpCount++;
        canJump = true;   
    }

}
