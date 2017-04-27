using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Local_Canvas : NetworkBehaviour
{

    public RectTransform healthBar;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        FindObjectOfType<Player_net>().GetComponent<Health>();	
	}
}
