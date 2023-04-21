using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRecords : MonoBehaviour
{
    public LevelRecords_SO recordData;

    #region Read From Records Data
    public bool Level1Complete
    {
        get { if (recordData != null) return recordData.level1Complete; else return false; }
        set { recordData.level1Complete = value; }
    }

    public float Level1Time
    {
        get { if (recordData != null) return recordData.level1Time; else return 0f; }
        set { recordData.level1Time = value; }
    }

    public int Level1Score
    {
        get { if (recordData != null) return recordData.level1Score; else return 0; }
        set { recordData.level1Score = value; }
    }

    public float Level1PointsPerSecond
    {
        get { if (recordData != null) return recordData.level1PointsPerSecond; else return 0f; }
        set { recordData.level1PointsPerSecond = value; }
    }

    public bool Level2Complete
    {
        get { if (recordData != null) return recordData.level2Complete; else return false; }
        set { recordData.level2Complete = value; }
    }

    public float Level2Time
    {
        get { if (recordData != null) return recordData.level2Time; else return 0f; }
        set { recordData.level2Time = value; }
    }

    public int Level2Score
    {
        get { if (recordData != null) return recordData.level2Score; else return 0; }
        set { recordData.level2Score = value; }
    }

    public float Level2PointsPerSecond
    {
        get { if (recordData != null) return recordData.level2PointsPerSecond; else return 0f; }
        set { recordData.level2PointsPerSecond = value; }
    }

    public bool Level3Complete
    {
        get { if (recordData != null) return recordData.level3Complete; else return false; }
        set { recordData.level3Complete = value; }
    }

    public float Level3Time
    {
        get { if (recordData != null) return recordData.level3Time; else return 0f; }
        set { recordData.level3Time = value; }
    }

    public int Level3Score
    {
        get { if (recordData != null) return recordData.level3Score; else return 0; }
        set { recordData.level3Score = value; }
    }

    public float Level3PointsPerSecond
    {
        get { if (recordData != null) return recordData.level3PointsPerSecond; else return 0f; }
        set { recordData.level3PointsPerSecond = value; }
    }
    #endregion

    public void UpdateRecords(int level, float time, int score)
    {
        switch(level)
        {
            case 1:
                if(!Level1Complete || score / time >= Level1PointsPerSecond)
                {
                    Level1Complete = true;
                    Level1Time = time;
                    Level1Score = score;
                    Level1PointsPerSecond = score / time;
                }
                break;
            case 2:
                if (!Level2Complete || score / time >= Level1PointsPerSecond)
                {
                    Level2Complete = true;
                    Level2Time = time;
                    Level2Score = score;
                    Level2PointsPerSecond = score / time;
                }
                break;
            case 3:
                if (!Level3Complete || score / time >= Level1PointsPerSecond)
                {
                    Level3Complete = true;
                    Level3Time = time;
                    Level3Score = score;
                    Level3PointsPerSecond = score / time;
                }
                break;
        }
    }
}
