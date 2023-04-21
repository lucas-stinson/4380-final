using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public List<GameObject> uiPages = new List<GameObject>();

    private void Start()
    {
        LoadPage(0);
    }

    public void LoadPage(int pageNum)
    {
        foreach (GameObject page in uiPages)
        {
            page.SetActive(false);
        }

        uiPages[pageNum].SetActive(true);
    }

}
