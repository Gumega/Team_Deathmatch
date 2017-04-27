using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{

    public int bulletDamage;
    public float bulletRange;

    public string ownerOfBullet;
    Vector3 startPosition;

    // Use this for initialization
    void Start()
    {
        startPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (ownerOfBullet == "")
        {
            Debug.Log("the bullet do not have name");

        }

        if (Vector3.Distance(gameObject.transform.position, startPosition) > bulletRange)
        {
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<Health>() != null)
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
            collision.gameObject.GetComponent<Player_net>().whoKill = ownerOfBullet;
        }
        Destroy(gameObject);
    }


}
