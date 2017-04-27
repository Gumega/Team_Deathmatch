using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Camera_net : MonoBehaviour
{

    public GameObject player;
    public GameObject gameCamera;
    public bool canSyncronize = true;
    public bool playerIsReady = false;

    void LateUpdate()
    {

        {
            if (canSyncronize && playerIsReady)
            {
                if (player == null)
                {
                    player = FindObjectOfType<Player_net>().gameObject;
                }
                else
                {
                    gameCamera.transform.position = new Vector3(player.transform.position.x, 10, player.transform.position.z);
                }
            }
        }
    }
}