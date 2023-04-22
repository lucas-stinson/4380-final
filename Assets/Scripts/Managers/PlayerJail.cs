using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJail : MonoBehaviour
{
    public KeyCode skipKey = KeyCode.R;

    public float delay;

    public GameManager manager;

    public Transform startPoint;

    public GameObject player;

    public static bool skipIntro;

    private void Awake()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        delay = skipIntro ? 0 : delay;
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
        player.transform.position = startPoint.position;
        manager.StartTimer();
        Destroy(this.gameObject);
    }

    public void ToggleSkip(bool input)
    {
        skipIntro = input;
    }
}
