using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPickup : MonoBehaviour
{
    public GameManager manager;

    private void Awake()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            manager.FinishLevel();
            Destroy(this.gameObject);
        }
    }
}
