using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlane : MonoBehaviour
{
    public Transform respawnPoint;
    public GameObject player;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            player.transform.position = respawnPoint.position;
        }
    }
}
