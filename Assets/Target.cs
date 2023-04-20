using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Renderer renderer; //This is used for changing the target color
    private AudioSource audioSource; //used to play the sound effect
    private GameObject shotBullet; //used to track the distance between target and bullet
    private float distanceAway; //same as above
    private Vector3 hitPoint;
    public float numCircles = 8;
    public int[] scores = new int[] {100, 80, 60, 50, 40, 30, 20, 10};
    private float scale;
    private bool alreadyShot = false;

    //These are the sound effects
    [Header("Sound effects")]
    public AudioClip greenEffect;
    public AudioClip yellowEffect;
    public AudioClip redEffect;
    // Start is called before the first frame update
    void Start()
    {
        renderer= GetComponent<Renderer>();
        audioSource= GetComponent<AudioSource>();
        scale = transform.localScale.x;
        Debug.Log(scale);
    }

    // Update is called once per frame
    void Update()
    {
        //if(shotBullet!= null)
        //{
        //    distanceAway = Vector3.Distance(transform.position, shotBullet.transform.position);
        //}
    }

    //we need to pass the distance to calculate score. The bullet and hit point are to track when the visual que should occur
    public void Shot(float distance, GameObject bullet, Vector3 point)
    {
        if(!alreadyShot)
        {
            alreadyShot = true;
            //shows already shot

            //rest has to happen in a coroutine since we want to wait until the bullet is close
            shotBullet = bullet;
            hitPoint = point;
            distanceAway = Vector3.Distance(transform.position, bullet.transform.position);
            StartCoroutine(WaitUntilClose(distance));

            
        } else
        {
            Debug.Log("Already shot");
        }
        
    }

    IEnumerator WaitUntilClose(float distance)
    {
        while (distanceAway > .5)
        {
            distanceAway = Vector3.Distance(hitPoint, shotBullet.transform.position);
            yield return new WaitForSeconds(.1f);
        }
        //Debug.Log("Done waiting");
        shotBullet.GetComponent<Bullet>().Die();
        //This is where the score is calculated
        int score = CalculateScore(distance);
        Debug.Log("Score increase: " + score);
        //change color and play a sound
        ChangeColor(score);
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

    //This function changes the color based on the score and plays the corresponding sound.
    private void ChangeColor(int score)
    {
        if(score >= 60)
        {
            renderer.material.color = Color.green;
            audioSource.clip= greenEffect;
            audioSource.Play();
        } else if(score >= 40) {
            renderer.material.color = Color.yellow;
            audioSource.clip = yellowEffect;
            audioSource.Play();
        } else
        {
            renderer.material.color = Color.red;
            audioSource.clip = redEffect;
            audioSource.Play();
        }
    }
}
