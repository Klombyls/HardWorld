using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakingDamage : MonoBehaviour
{
    public float damage = 7f;
    public string collisionTag;


    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == collisionTag) 
        {
            HealthBar health = coll.gameObject.GetComponent<HealthBar>();
            health.TakingDamage(damage);
        }
    }
}
