using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordStrike : MonoBehaviour
{

    [SerializeField]
    GameObject attackHitBox;

    public float RotateSpeed;
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
        attackHitBox.transform.rotation = Quaternion.identity;

        isAttacking = false;
    }
}
