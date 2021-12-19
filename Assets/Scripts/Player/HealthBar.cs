using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Image Bar;
    public float health = 100f;
    public float maxHealth = 100f;
    public Inventory playerInventory;

    public void TakingDamage(float damage)
    {
        health -= damage;
    }

    public void RecoveryPotion(float heal)
    {
        health += heal;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    void Update()
    { 
        if (health <= 0)
        {
            playerInventory.InventoryDropout();
            gameObject.SetActive(false);
            Invoke("RevivalPlayer", 5f);
        }

        health += Time.deltaTime * 1f;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
        Bar.fillAmount = health / 100;
    }

    private void RevivalPlayer()
    {
        transform.position = new Vector3(1500, 300);
        health = 100f;
        gameObject.SetActive(true);
    }
}
