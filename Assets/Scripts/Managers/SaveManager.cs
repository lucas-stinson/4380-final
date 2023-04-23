using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private static bool firstLoad = true;
    public LevelRecords_SO records;

    private void Start()
    {
        if (firstLoad)
        {
            LoadRecordData();
            firstLoad = false;
        } 
        else
        {
            SaveRecordData();
        }
    }

    void Save(object data, string key)
    {
        string jsonData = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(key, jsonData);
        PlayerPrefs.Save();
    }

    void Load(object data, string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), data);
        }
    }

    void SaveRecordData()
    {
        Save(records, "Bullet's Edge Level Records");
    }

    void LoadRecordData()
    {
        Load(records, "Bullet's Edge Level Records");
    }

    public void ClearSave()
    {
        records.level1Complete = false;
        records.level1Time = 0f;
        records.level1Score = 0;
        records.level1PointsPerSecond = 0f;

        records.level2Complete = false;
        records.level2Time = 0f;
        records.level2Score = 0;
        records.level2PointsPerSecond = 0f;

        records.level3Complete = false;
        records.level3Time = 0f;
        records.level3Score = 0;
        records.level3PointsPerSecond = 0f;

        Save(records, "Bullet's Edge Level Records");
    }
}
