using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJail : MonoBehaviour
{
    public KeyCode skipKey = KeyCode.R;

    public float delay;
    public AudioClip levelStart;

    public GameManager manager;
    private AudioSource audioSource;
    private AudioSource music;

    public Transform startPoint;

    public GameObject player;

    public static bool skipIntro;

    private void Awake()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        music = GameObject.Find("Music").GetComponent<AudioSource>();
    }

    private void Start()
    {
        delay = skipIntro ? 0 : delay;
        if(delay > 0 )
        {
            music.Stop();
            audioSource.clip = levelStart;
            audioSource.Play();
        }
        Invoke("StartLevel", delay);
        
    }

    private void Update()
    {
        if (!manager.paused)
        {
            if (Input.GetKey(skipKey))
            {
                StartLevel();
            }
        }
    }

    public void StartLevel()
    {
        music.Play();
        player.transform.position = startPoint.position;
        manager.StartTimer();
        Destroy(this.gameObject);
    }

    public void ToggleSkip(bool input)
    {
        skipIntro = input;
    }
}
