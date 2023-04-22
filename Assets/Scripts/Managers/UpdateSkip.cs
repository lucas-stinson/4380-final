using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSkip : MonoBehaviour
{
    public Toggle button;

    private void Awake()
    {
        button = GetComponent<Toggle>();
    }

    public void UpdateSkipLevel()
    {
        PlayerJail.skipIntro = button.isOn;
    }
}
