using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_net : MonoBehaviour
{
    [HeaderAttribute("Player Properties")]
    public string playerName;
    public float speed;
    public string whoKill;
    [SpaceAttribute(20)]
    [Header("Guns")]
    public Transform bulletTransformInicial;
    float bulletSpeed = 600;
    public GameObject[] weappons;
    public GameObject gunBullet, arrow, machineGunBullet, shotGunBullet, bazookagun;
    public int inHandWeaponIndex = 0;
    public int slotWeaponIndex = 1;
    public int[] maxBulletsIMagazines;
    public int[] bulletsInMagezines;
    public int[] totalBullets;
    public bool reload;
    float counterForReload;
    float counterPerBullet;
    public float timeOfReload;
    public float[] timePerBullet;
    public int totalBulletsReload;
    [SpaceAttribute(20)]
    [Header("UI Elements")]
    public GameObject canvas;
    public Sprite[] guns;
    public Image inHandWeaponImage;
    public Image savedWeaponImage;
    public Text inHandBulletQuantityText;
    public Text savedBulletQuantityText;
    public GameObject deathPanel;
    public GameObject meshsRenderes;
    public GameObject HUDPanel;
    public GameObject reloadCircle;

    public GameObject[] spawnPoints;
    public bool isLocalPlayer;
    void Start()
    {
        if (!isLocalPlayer)
        {
            deathPanel.SetActive(true);
            canvas.SetActive(false);
            return;
        }
        FindObjectOfType<Camera_net>().playerIsReady = true;
        spawnPoints = GameObject.FindGameObjectsWithTag("RespawnPoints");
        meshsRenderes.SetActive(true);
        reloadCircle.SetActive(false);
        HUDPanel.SetActive(true);
        deathPanel.SetActive(false);
        refreshAmmoQuantityAndUI();
        reload = false;
    }
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.LookAt(new Vector3(mouseWorldPosition.x, transform.position.y, mouseWorldPosition.z));
        if (reload)
        {
            Reload();
        }
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            this.gameObject.GetComponent<Health>().TakeDamage(10);

        }
        if (Input.GetMouseButtonDown(0))
        {
            Attack();

            /*
                        if (inHandWeaponIndex == 1)
                        {
                            if (bulletsInMagezines[1] > 0)
                            {
                                bulletsInMagezines[1]--;
                                CmdShoot();
                                refreshAmmoQuantityAndUI();
                            }
                            else
                            {
                                //son de sem bala
                            }
                        }*/
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (reload)
            {
                StopReload();
            }
            ChangeWeapon();
        }
        if (Input.GetKeyDown(KeyCode.R) && !reload)
        {
            calculateTimeReload();
            reload = true;
        }
    }

    public void Attack()
    {
        Weappon weapponComponent = weappons[inHandWeaponIndex].GetComponent<Weappon>();
        if (reload)
        {
            StopReload();
        }
        if (weapponComponent.isMelee)
        {

        }
        else if (weapponComponent.isMachineGun)
        {

        }
        else
        {
            if (weapponComponent.bulletsInMagazine > 0)
            {
                weapponComponent.bulletsInMagazine--;
                CmdShoot();
                refreshAmmoQuantityAndUI();
            }
        }
    }

    public void calculateTimeReload()
    {
        Weappon weapponComponentInHand = weappons[inHandWeaponIndex].GetComponent<Weappon>();

        int bulletsToReload = weapponComponentInHand.maxBulletInMagazine - weapponComponentInHand.bulletsInMagazine;
        int howManyBulletsAreMissing = bulletsToReload - weapponComponentInHand.totalBullets;
        if (howManyBulletsAreMissing <= 0)
        {
            howManyBulletsAreMissing = 0;
        }
        bulletsToReload -= howManyBulletsAreMissing;
        timeOfReload = bulletsToReload * weapponComponentInHand.timeReloadPerBullet;
    }
    public void Reload()
    {
        Weappon weapponComponentInHand = weappons[inHandWeaponIndex].GetComponent<Weappon>();

        if (!weapponComponentInHand.isMelee)
        {
            counterForReload += Time.deltaTime;
            counterPerBullet += Time.deltaTime;
            reloadCircle.SetActive(true);
            reloadCircle.GetComponent<Image>().fillAmount = counterForReload / timeOfReload;

            if (counterPerBullet > weapponComponentInHand.timeReloadPerBullet && weapponComponentInHand.totalBullets > 0 && weapponComponentInHand.bulletsInMagazine < weapponComponentInHand.maxBulletInMagazine)
            {
                weapponComponentInHand.bulletsInMagazine++;
                weapponComponentInHand.totalBullets--;
                refreshAmmoQuantityAndUI();
                counterPerBullet = 0;
                //sound reload
            }
            else if (weapponComponentInHand.totalBullets <= 0 || weapponComponentInHand.bulletsInMagazine >= weapponComponentInHand.maxBulletInMagazine)
            {
                StopReload();
            }
        }
        else
        {
            reload = false;
        }
    }
    public void ChangeWeapon()
    {
        int tempPrimaryWeaponIndex = inHandWeaponIndex;
        inHandWeaponIndex = slotWeaponIndex;
        slotWeaponIndex = tempPrimaryWeaponIndex;
        for (int i = 0; i < weappons.Length; i++)
        {
            CmdChangeWeappon(i, false);
        }
        CmdChangeWeappon(inHandWeaponIndex, true);
        refreshAmmoQuantityAndUI();
    }
    public void StopReload()
    {
        counterPerBullet = 0;
        counterForReload = 0;
        reloadCircle.SetActive(false);
        reload = false;
    }
    public void refreshAmmoQuantityAndUI()
    {
        Weappon weapponComponetInHand = weappons[inHandWeaponIndex].GetComponent<Weappon>();
        Weappon weapponComponetSlot = weappons[slotWeaponIndex].GetComponent<Weappon>();


        inHandWeaponImage.sprite = weapponComponetInHand.uiImage;
        savedWeaponImage.sprite = weapponComponetSlot.uiImage;

        inHandBulletQuantityText.text = weapponComponetInHand.bulletsInMagazine + "/" + weapponComponetInHand.totalBullets;
        savedBulletQuantityText.text = weapponComponetSlot.bulletsInMagazine + "/" + weapponComponetSlot.totalBullets;
    }

    public void CmdChangeWeappon(int index, bool active)
    {
        RpcChangeWeappon(index, active);
    }


    public void RpcChangeWeappon(int index, bool active)
    {
        this.weappons[index].SetActive(active);
    }


    public void CmdShoot()
    {
        GameObject instance = Instantiate(weappons[inHandWeaponIndex].GetComponent<Weappon>().bullet, weappons[inHandWeaponIndex].GetComponent<Weappon>().bulletPosition.position, weappons[inHandWeaponIndex].GetComponent<Weappon>().bulletPosition.rotation) as GameObject;
        instance.GetComponent<Bullet>().ownerOfBullet = playerName;
        instance.GetComponent<Rigidbody>().AddForce(instance.transform.up * weappons[inHandWeaponIndex].GetComponent<Weappon>().bulletSpeed);
        //NetworkServer.Spawn(instance);
    }

    public void RpcDeath()
    {
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        this.gameObject.transform.position = GameObject.FindWithTag("DeathZone").transform.position;
        this.deathPanel.SetActive(true);
        this.HUDPanel.SetActive(false);
    }
    public void Respawn()
    {
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

        this.meshsRenderes.SetActive(true);
        this.gameObject.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].gameObject.transform.position;
        this.deathPanel.SetActive(false);
        this.HUDPanel.SetActive(true);
        this.gameObject.GetComponent<Health>().CmdReloadLife();
    }
}
