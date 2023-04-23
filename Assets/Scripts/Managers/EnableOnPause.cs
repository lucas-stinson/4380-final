using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnPause : MonoBehaviour
{
    public GameManager manager;

    public List<GameObject> disableWhenPaused = new List<GameObject>();
    public List<GameObject> enableWhenPaused = new List<GameObject>();

    private void Awake()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void StartPause()
    {
        foreach (GameObject item in disableWhenPaused)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in enableWhenPaused)
        {
            item.SetActive(true);
        }
    }

    public void StopPause()
    {
        Debug.Log("Start of function");
        foreach (GameObject item in disableWhenPaused)
        {
            Debug.Log("enabling an item");
            item.SetActive(true);
        }
        Debug.Log("enabled all items");
        foreach (GameObject item in enableWhenPaused)
        {
            Debug.Log("disabling an item");
            item.SetActive(false);
        }
        Debug.Log("disabled all items");
    }
}
