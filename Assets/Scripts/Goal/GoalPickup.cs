using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPickup : MonoBehaviour
{
    public GameManager manager;

    public Material complete;
    public Material incomplete;
    private Material materialToApply;

    private void Awake()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        ChangeFlagMaterial();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && manager.levelComplete)
        {
            manager.FinishLevel();
            Destroy(this.gameObject);
        }
    }

    public void ChangeFlagMaterial()
    {
        if(manager.levelComplete)
        {
            materialToApply = complete;
        } 
        else
        {
            materialToApply = incomplete;
        }

        Transform[] children = GetComponentsInChildren<Transform>();

        foreach (Transform child in children)
        {
            if (child != transform)
            {
                Renderer renderer = child.GetComponent<Renderer>();

                if (renderer != null)
                {
                    renderer.material = materialToApply;
                }
            }
        }

    }
    
}
