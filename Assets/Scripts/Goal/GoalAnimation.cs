using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalAnimation : MonoBehaviour
{
    public float amplitude = 0.5f; 
    public float frequency = 1.0f; 
    public float rotationSpeed = 50.0f;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.position = startPosition + Vector3.up * Mathf.Sin(Time.time * frequency) * amplitude;
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }
}
