using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int levelNumber;

    public float levelTime = 0;

    public int levelScore = 0;

    public bool timing;

    public LevelRecords record;

    public void Start()
    {
        levelTime = 0;
    }

    public void StartTimer()
    {
        timing = true;
    }

    private void Update()
    {
        if(timing)
        {
            levelTime += Time.deltaTime;
        }
    }

    public void StopTimer()
    {
        timing = false;
    }

    public void FinishLevel()
    {
        StopTimer();
        record.UpdateRecords(levelNumber, levelTime, levelScore);
    }
}
