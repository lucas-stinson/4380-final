using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float fireRate = 0.5f;
    public float lastFire = 0f;
    private bool fire;

    // Start is called before the first frame update
    void Start()
    {
        lastFire = Time.timeSinceLevelLoad;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        if(fire)
        {
            Fire();
        }
    }


    private void GetInput()
    {
        fire = Input.GetMouseButton(0);
    }

    private void Fire()
    {
        if(Time.timeSinceLevelLoad > lastFire + fireRate)
        {
            lastFire = Time.timeSinceLevelLoad;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                Debug.Log("Hit " + hit.transform.name);
                GameObject objectHit = hit.collider.gameObject;
                if(objectHit.tag == "Target") //confirming the object hit was a target
                {
                    Vector3 hitPoint = hit.point; //This is the point where the object was hit
                    float distance = Vector3.Distance(objectHit.transform.position, hitPoint);
                    //Debug.Log("Distance from center: " + distance);
                    Debug.Log("Hit point: " + hitPoint + ", Target position: " + objectHit.transform.position + ", Distance: " + distance);
                    //Debug.Log("target position: " + objectHit.transform.position);

                    hit.collider.gameObject.GetComponent<Target>().Shot(distance); //We pass the distance from center to the function
                }
            }
        }
        
    }
}
