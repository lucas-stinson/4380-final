using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRecords : MonoBehaviour
{
    public LevelRecords_SO recordData;

    #region("Read From Records Data")
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
#endregion

    public void UpdateRecords(int level, float time, int score)
    {
        switch(level)
        {
            case 1:
                if(!Level1Complete || time <= Level1Time && score >= Level1Score)
                {
                    Level1Complete = true;
                    Level1Time = time;
                    Level1Score = score;
                }
                break;
            case 2:
                if (!Level2Complete || time <= Level1Time && score <= Level2Score)
                {
                    Level2Complete = true;
                    Level2Time = time;
                    Level2Score = score;
                }
                break;
            case 3:
                if (!Level3Complete || time <= Level1Time && score <= Level3Score)
                {
                    Level3Complete = true;
                    Level3Time = time;
                    Level3Score = score;
                }
                break;
        }
    }
}
