using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : MonoBehaviour
{

    [Header("Input")]
    public KeyCode slideKey = KeyCode.LeftControl;
    private float horizontalInput;
    private float verticalInput;

    [Header("References")]
    public Transform orientation;
    public Transform playerObj;
    private Rigidbody rb;
    private PlayerMovement movement;

    [Header("Sliding Variables")]
    public float maxSlideTime;
    public float slideForce;
    private float slideTimer;
    public float slideYScale;
    private float startYScale;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        movement = GetComponent<PlayerMovement>();

        startYScale = playerObj.localScale.y;
    }
    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(slideKey) && (horizontalInput != 0 || verticalInput != 0))
        {
            StartSlide();
        }
        if (Input.GetKeyUp(slideKey) && movement.sliding)
        {
            StopSlide();
        }
    }
    private void FixedUpdate()
    {
        if(movement.sliding)
        {
            SlideMovement();
        }
    }
    private void StartSlide()
    {
        movement.sliding = true;
        playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        slideTimer = maxSlideTime;

    }
    private void SlideMovement()
    {
        Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (!movement.OnSlope() || rb.velocity.y > -0.1f)
        { 
            rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);

            slideTimer -= Time.deltaTime;
        } 
        else
        {
            rb.AddForce(movement.GetSlopeMoveDirection(inputDirection) * slideForce * 10f, ForceMode.Force);
        }
        if(slideTimer <= 0)
        {
            StopSlide();
        }
    }
    private void StopSlide()
    {
        movement.sliding = false;
        playerObj.localScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);

    }
}
