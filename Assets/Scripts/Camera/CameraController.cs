using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;
    public Transform camHolder;

    float xRotation, yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        sensX = CameraOptions.currentSensX;        
        sensY = CameraOptions.currentSensY;        
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * sensX * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensY * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //move the camera, then rotate the player
        camHolder.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

    }
    
    public void ChangeFov(float val)
    {
        GetComponent<Camera>().DOFieldOfView(val, 0.25f); //tweening package is used for these camera movements
    }
    public void Tilt(float val)
    {
        transform.DOLocalRotate(new Vector3(0, 0, val), 0.25f);
    }
    
}
