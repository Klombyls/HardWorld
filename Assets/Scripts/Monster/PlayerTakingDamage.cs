using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakingDamage : MonoBehaviour
{
    public HealthBar health;
    public float damage;

    public GameObject player;

    private void Update()
    {
        if (player.activeSelf
            && Vector3.Distance(transform.position, player.transform.position) <= 4)
        {
            if (health.canTakeDamage)
            {
                health.TakingDamageFromMonster(damage);
            }
        }
    }
}
