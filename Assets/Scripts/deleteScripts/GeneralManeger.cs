using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralManeger : MonoBehaviour {

    public Player_net player;
    public Image healthBar;
    public Image bulletProofBar, bulletProofBarBackground;

    void Start ()
    {
	}
	
	void Update ()
    {/*
        if (player==null)
        {
            player = FindObjectOfType<Player_net>();
        }
        else
        {
            healthBar.fillAmount = player.health / 100;
            if (player.health > 50)
            {
                healthBar.color = Color.green;
            }
            if (player.health <= 50 && player.health > 25)
            {
                healthBar.color = Color.yellow;
            }
            if (player.health <= 25)
            {
                healthBar.color = Color.red;
            }
            if (player.bulletProof < 1)
            {
                bulletProofBarBackground.gameObject.SetActive(false);
            }
            else
            {
                bulletProofBarBackground.gameObject.SetActive(true);
                bulletProofBar.fillAmount = player.bulletProof / 100;
            }
        }*/
	}
}
