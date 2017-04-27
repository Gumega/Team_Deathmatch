using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Health : MonoBehaviour
{
    public const int maxHealth = 100;    
    public int currentHealth = maxHealth;
    public RectTransform healthBar;

    //public RectTransform healthBarHUD;

    void Start()
    {
        //healthBarHUD = GameObject.FindGameObjectWithTag("HUD").GetComponent<RectTransform>();

    }

    public void TakeDamage(int damageValue)
    {
        currentHealth -= damageValue;
        if (currentHealth <= 0)
        {
            gameObject.GetComponent<Player_net>().RpcDeath();
            currentHealth = 0;
            Debug.Log("Dead");
        }
        //healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
        //healthBarHUD.sizeDelta = new Vector2(currentHealth * 2, healthBarHUD.sizeDelta.y);
    }

    void OnChangeHealth(int health)
    {
        //healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
        //healthBarHUD.sizeDelta = new Vector2(health * 2, healthBarHUD.sizeDelta.y);

    }
   
    public void CmdReloadLife()
    {
        currentHealth = maxHealth;
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }


}
