using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateVictoryScreen : MonoBehaviour
{
    public TMP_Text levelName;
    public TMP_Text timeText;
    public TMP_Text scoreText;

    public void LevelFinish(int levelNum, float time, float score)
    {
        this.gameObject.SetActive(true);

        levelName.text = "You Beat\nLevel " + levelNum;

        int seconds = Mathf.FloorToInt(time);
        int centiseconds = Mathf.FloorToInt((time * 100) % 100);
        timeText.text = "Time: " + seconds + "<size=14>" + centiseconds + "</size>";

        scoreText.text = "Score: " + score;
    }
}
