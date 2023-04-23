using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurotialAudio : MonoBehaviour
{
    public AudioClip[] startingClips;
    public AudioClip[] targetClips;
    public AudioClip[] audioClips;
    private AudioSource audioSource;
    private GameObject player;
    private float[] distances = new float[5];
    public Vector3[] points;
    private bool[] played = { false, false, false, false, false };
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(StartingClips());
    }

    private void Awake()
    {

        player = GameObject.Find("Player");
        if(player == null)
        {
            Debug.Log("Player null");
        }
    }

    IEnumerator StartingClips()
    {
        for (int i = 0; i < startingClips.Length; i++)
        {
            audioSource.clip = startingClips[i];
            audioSource.Play();
            while (audioSource.isPlaying)
            {
                yield return null;
            }
        }
    }

    IEnumerator TargetClips()
    {
        for (int i = 0; i < targetClips.Length; i++)
        {
            audioSource.clip = targetClips[i];
            audioSource.Play();
            while (audioSource.isPlaying)
            {
                yield return null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        int x = 0;
        foreach(Vector3 point in points)
        {
            //Debug.Log(player.transform.position + " and point:" + point);
            distances[x] = Vector3.Distance(point, player.transform.position);
            //Debug.Log("Distance " + x + " " + distances[x]);
            if (distances[x] < 10 && played[x] == false)
            {
                audioSource.Stop();
                played[x] = true;
                if(x == 4)
                {
                    StartCoroutine(TargetClips());
                    break;
                }
                audioSource.clip = audioClips[x];
                audioSource.Play();
                break;
            }
            x++;
        }
    }
}
