using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public KeyCode pauseKey = KeyCode.Backspace;

    [Header("References")]
    public LevelRecords record;
    public GoalPickup flag;
    public EnableOnPause set;
    public UpdateVictoryScreen victoryMenu;

    [Header("Targets")]
    public int targetsInLevel;
    public int targetsHit;

    [Header("Tracking")]
    public int levelNumber;
    public float levelTime = 0;
    public int levelScore = 0;
    public bool timing;
    public bool levelComplete = false;   
    public bool paused;

    private void Awake()
    {
        set = GetComponent<EnableOnPause>();
        record = GetComponent<LevelRecords>();
    }

    private void Start()
    {
        levelTime = 0;
    }

    private void Update()
    {
        if (timing)
        {
            levelTime += Time.deltaTime;
        }

        if (Input.GetKeyDown(pauseKey))
        {
            if(paused)
            {
                //StopPause(); we got a button now
            }
            else
            {
                StartPause();
            }
        }
    }

    

    public void StartTimer()
    {
        timing = true;
    }

    public void StopTimer()
    {
        timing = false;
    }

    public void FinishLevel()
    {
        StopTimer();

        Time.timeScale = 0f;
        paused = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        victoryMenu.LevelFinish(levelNumber, levelTime, levelScore);
        record.UpdateRecords(levelNumber, levelTime, levelScore);
        if(levelNumber == 3)
        {
            //play level 3 complete audio
            GameObject.Find("LevelEndAudio").GetComponent<AudioSource>().Play();
            GameObject.Find("Music").GetComponent<AudioSource>().volume = 0.25f;
        }
    }

    public void HitTarget(int score)
    {
        levelScore += score;
        targetsHit++;
        CheckLevelComplete();
    }

    public bool CheckLevelComplete()
    {
        if(targetsInLevel <= targetsHit)
        {
            levelComplete = true;
            flag.ChangeFlagMaterial();
        }
        return levelComplete;
    }

    public void StartPause()
    {
        Time.timeScale = 0f;

        timing = false;
        paused = true;
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        set.StartPause();
    }

    public void StopPause()
    {
        Time.timeScale = 1f;

        timing = true;
        paused = false;
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        set.StopPause();
    }
}
