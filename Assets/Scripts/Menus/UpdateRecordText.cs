using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UpdateRecordText : MonoBehaviour
{
    public LevelRecords recordData;

    public int levelNum;

    public TMP_Text runTitle;
    public TMP_Text timeText;
    public TMP_Text scoreText;

    private void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        switch (levelNum)
        {
            case 1:
                if (recordData.Level1Complete)
                {
                    int seconds = Mathf.FloorToInt(recordData.Level1Time);
                    int centiseconds = Mathf.FloorToInt((recordData.Level1Time * 100) % 100);
                    timeText.text = "Time: " + seconds + "<size=14>" + centiseconds + "</size>";
                    scoreText.text = "Score: " + recordData.Level1Score;
                }
                else
                {
                    runTitle.text = "Not Completed";
                    runTitle.rectTransform.anchoredPosition = new Vector3(60, -15, 0);
                    timeText.text = "";
                    scoreText.text = "";
                }
                break;
            case 2:
                if (recordData.Level2Complete)
                {
                    int seconds = Mathf.FloorToInt(recordData.Level2Time);
                    int centiseconds = Mathf.FloorToInt((recordData.Level2Time * 100) % 100);
                    timeText.text = "Time: " + seconds + "<size=14>" + centiseconds + "</size>";
                    scoreText.text = "Score: " + recordData.Level2Score;
                }
                else
                {
                    runTitle.text = "Not Completed";
                    runTitle.rectTransform.anchoredPosition = new Vector3(60, -15, 0);
                    timeText.text = "";
                    scoreText.text = "";
                }
                break;
            case 3:
                if (recordData.Level3Complete)
                {
                    int seconds = Mathf.FloorToInt(recordData.Level3Time);
                    int centiseconds = Mathf.FloorToInt((recordData.Level3Time * 100) % 100);
                    timeText.text = "Time: " + seconds + "<size=14>" + centiseconds + "</size>";
                    scoreText.text = "Score: " + recordData.Level3Score;
                }
                else
                {
                    runTitle.text = "Not Completed";
                    runTitle.rectTransform.anchoredPosition = new Vector3(60, -15, 0);
                    timeText.text = "";
                    scoreText.text = "";
                }
                break;
        }
    }
}
