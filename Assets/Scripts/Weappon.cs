using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weappon : MonoBehaviour
{
    public bool isMachineGun;
    public bool isMelee;
    public Sprite uiImage;
    public GameObject bullet;
    public Transform bulletPosition;
    public int maxBulletInMagazine;
    public int bulletsInMagazine;
    public int totalBullets;
    public float timeReloadPerBullet;
    public float bulletSpeed;





    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
