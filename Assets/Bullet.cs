using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Tooltip("The distance this bullet will move each second in meters.")]
    public float bulletSpeed = 3.0f;

    [Tooltip("How far away from the main camera before destroying the bullet gameobject in meters.")]
    public float destroyDistance = 20.0f;

    /// <summary>
    /// Description:
    /// Standard Unity function called once per frame
    /// Inputs: 
    /// none
    /// Returns: 
    /// void (no return)
    /// </summary>
    private void Update()
    {
        MoveBullet();
    }

    /// <summary>
    /// Description:
    /// Move the projectile in the direction it is heading
    /// Inputs: 
    /// none
    /// Returns: 
    /// void (no return)
    /// </summary>
    private void MoveBullet()
    {
        // move the transform
        transform.position = transform.position + transform.forward * bulletSpeed * Time.deltaTime;

        // calculate the distance from the main camera
        float dist = Vector3.Distance(Camera.main.transform.position, transform.position);

        // if the distance is greater than the destroyDistance
        if (dist > destroyDistance)
        {
            Destroy(this.gameObject); // destroy the gameObject
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
