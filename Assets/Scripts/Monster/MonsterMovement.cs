using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{

    public float speed = 1f;

    public float thrust = 2250;
    public string nameMonster;
    protected bool timeJump = true;

    private bool movingRight = true;

    public float rayDistance = 1.5f;

    public Transform playerPosition;

    public Animator animator;

    private Rigidbody2D rb2D;

    public bool flip = false;

    protected void ReloadTimeForJump()
    {
        timeJump = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        Physics2D.queriesStartInColliders = false;
    }

    // Update is called once per frame
    void Update()
    {
        rb2D.AddForce(transform.up * 3);

        if (transform.position.x >= playerPosition.position.x + Random.Range(2, 4))
        {
            if (flip)
                transform.eulerAngles = new Vector3(0, -180, 0);
            else transform.eulerAngles = new Vector3(0, 0, 0);
            movingRight = false;
            rb2D.velocity = new Vector2(-speed * 25, rb2D.velocity.y);
            animator.Play(nameMonster + "Animation");

        }
        else if (transform.position.x <= playerPosition.position.x + Random.Range(-2, -4)) {
            if (flip)
                transform.eulerAngles = new Vector3(0, 0, 0);
            else transform.eulerAngles = new Vector3(0, -180, 0);
            movingRight = true;
            rb2D.velocity = new Vector2(speed * 25, rb2D.velocity.y);
            animator.Play(nameMonster + "Animation");
        }

        if (movingRight)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.right, rayDistance);
            if (hit.collider != null && timeJump)
            {
                rb2D.AddForce(transform.up * thrust);
                timeJump = false;
                Invoke("ReloadTimeForJump", 1f);
            }
        }
        else if (!movingRight) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.left, rayDistance);

            if (hit.collider != null && timeJump)
            {
                rb2D.AddForce(transform.up * thrust);
                timeJump = false;
                Invoke("ReloadTimeForJump", 1f);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.right * rayDistance);
    }
}
