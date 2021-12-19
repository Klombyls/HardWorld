using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Image Bar;
    public float health = 100f;
    public float maxHealth = 100f;


    public void TakingDamage(int damage)
    {
        health -= damage;

        if (health < 0)
        {
            health = 0;
        }
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
        health += Time.deltaTime * 1f;

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        Bar.fillAmount = health / 100;
    }
}
