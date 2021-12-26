using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordStrike : MonoBehaviour
{

    [SerializeField]
    GameObject attackHitBox;

    [SerializeField]
    GameObject attacksPose;

    public float RotateSpeed;
    public Transform attackPose;
    public float attackRange;
    public LayerMask Monster;
    public float dmg = 10f;

    bool isAttacking = false;

    private void Start()
    {
        attackHitBox.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            isAttacking = true;

            StartCoroutine(DoAttack());

            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPose.position, attackRange, Monster);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<MonsterTakingDamage>().TakingDmg(dmg);
            }
        }
    }

    void FixedUpdate()
    {
        float angle = transform.eulerAngles.z;

        if (isAttacking == true)
        {
            if (Input.GetKey("a"))
            {
                attackHitBox.transform.Rotate(0, 0, RotateSpeed * 1f * Time.deltaTime);
            }
            else
            {
                attackHitBox.transform.Rotate(0, 0, -(RotateSpeed * 1f * Time.deltaTime));
            }
        }
    }

    IEnumerator DoAttack()
    {
        attackHitBox.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        attackHitBox.SetActive(false);
        attackHitBox.transform.rotation = Quaternion.Euler(0f, 0f, 34.27f);

        isAttacking = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPose.position, attackRange);
    }
}
