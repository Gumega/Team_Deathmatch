using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet_net : NetworkBehaviour {

    public float speed;
    public float bulletRange;
    public float bulletDamage;
    Vector3 inicialPosition;
     

	// Use this for initialization
	void Start ()
    {
        inicialPosition = gameObject.transform.position;
	}

   [ServerCallback]
	void Update ()
    {
        gameObject.transform.Translate(Vector3.up * speed * Time.deltaTime);
        if (Vector3.Distance(gameObject.transform.position, inicialPosition) > bulletRange)
        {
            NetworkServer.Destroy(this.gameObject);

        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        /*
        if (other.gameObject.tag == "Player")
        {
            if (other.GetComponent<Player_net>().bulletProof >= 1)
            {
                other.GetComponent<Player_net>().bulletProof -= bulletDamage;
            }
            else
            {
                other.GetComponent<Player_net>().health -= bulletDamage;
            }
            NetworkServer.Destroy(this.gameObject);
        }*/
    }
}
