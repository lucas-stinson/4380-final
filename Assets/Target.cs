using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer= GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shot(float distance)
    {
        renderer.material.color = Color.green;
        Debug.Log("Score increase: " + CalculateScore(distance));
    }

    private int CalculateScore(float distance)
    {
        return (int)Mathf.Round(distance);
    }
}
