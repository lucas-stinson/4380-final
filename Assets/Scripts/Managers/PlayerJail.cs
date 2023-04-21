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

    private void Awake()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
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
}
