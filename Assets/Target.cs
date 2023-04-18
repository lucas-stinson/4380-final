using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Renderer renderer;
    public float numCircles = 8;
    public int[] scores = new int[] {100, 80, 60, 50, 40, 30, 20, 10};
    private float scale;
    private bool alreadyShot = false;
    // Start is called before the first frame update
    void Start()
    {
        renderer= GetComponent<Renderer>();
        scale = transform.localScale.x;
        Debug.Log(scale);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shot(float distance)
    {
        if(!alreadyShot)
        {
            alreadyShot = true;
            //shows already shot
            renderer.material.color = Color.green;
            //This is where the score is calculated
            Debug.Log("Score increase: " + CalculateScore(distance));
        } else
        {
            Debug.Log("Already shot");
        }
        
    }

    private int CalculateScore(float distance)
    {
        //return (int)Mathf.Round(distance);

        //Determine the size of the target based on the scale
        //We want a different score for each circle, not just based on distance
        float radius = scale / 2;
        for(int i = 1; i < scores.Length; i++)
        {
            //Debug.Log("Distance: " + distance + ", radius/numcircles*i " + (radius / numCircles * i));
            if(distance < radius/ numCircles * i)
            {
                return scores[i-1];
            }
        }
        return scores.Last();

    }
}
