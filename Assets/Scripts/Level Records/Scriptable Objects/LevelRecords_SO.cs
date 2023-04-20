using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Records", menuName = "Level Records")]
public class LevelRecords_SO : ScriptableObject
{
    [Header("Level 1")]
    public bool level1Complete;
    public float level1Time;
    public int level1Score;

    [Header("Level 2")]
    public bool level2Complete;
    public float level2Time;
    public int level2Score;

    [Header("Level 3")]
    public bool level3Complete;
    public float level3Time;
    public int level3Score;
}
