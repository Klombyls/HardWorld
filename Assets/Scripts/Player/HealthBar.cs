using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Image Bar;
    public bool canTakeDamage = true;
    public float health = 100f;
    public float maxHealth = 100f;
    public Inventory playerInventory;
    private float heal = 20f;
    private bool canUseRecoveryPotion = true;


    private void ReloadTimeForDamage()
    {
        canTakeDamage = true;
    }

    private void ReloadTimeForRecoveryPotion()
    {
        canUseRecoveryPotion = true;
    }

    public void TakingDamageFromMonster(float damage)
    {
        if (playerInventory.idArmor != 0) health -= damage * (1 - (playerInventory.idArmor - 9) * 0.2f);
        else health -= damage;
        canTakeDamage = false;
        Invoke("ReloadTimeForDamage", 0.5f);
    }

    public void TakingDamageFromFall(float damage)
    {
        health -= damage;
        canTakeDamage = false;
        Invoke("ReloadTimeForDamage", 0.5f);
    }

    public void RecoveryPotion()
    {
        Inventory inv = GameObject.Find("Main Camera").GetComponent<Inventory>();

        if (canUseRecoveryPotion)
        {
            inv.items[inv.currentUseID - 1].count--;
            if (inv.items[inv.currentUseID - 1].count == 0)
                inv.items[inv.currentUseID - 1].id = 0;
            inv.UpdateInventory();

            health += heal;

            if (health > maxHealth)
            {
                health = maxHealth;
            }
            canUseRecoveryPotion = false;
            Invoke("ReloadTimeForRecoveryPotion", 20f);
        }

    }

    void Update()
    { 
        if (health <= 0)
        {
            playerInventory.InventoryDropout();
            gameObject.SetActive(false);
            playerInventory.idArmor = playerInventory.items[36].id;
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
