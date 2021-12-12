using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator animator;
    public Rigidbody2D rb2D;
    public SpriteRenderer sr;
    public float thrust = 1f;
    public float speed = 1f;

    protected bool canJump = true;
    protected bool timeJump = true;
    protected bool strafeLeft = false;
    protected bool strafeRight = false;
    protected bool doJump = false;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }
    protected void ReloadTimeForJump()
    {
        timeJump = true;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Block"){
            canJump = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            canJump = true;
        }
    }
    private void OnCollisionExit2D()
    {
        canJump = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("a")) {
            strafeLeft = true;
        } else {
            strafeLeft = false;
        }
        if (Input.GetKey("d")) {
            strafeRight = true;
        } else {
            strafeRight = false;
        }
        if (Input.GetKey("space")) {
            doJump = true;
        }
    }

    private void FixedUpdate()
    {
        if(doJump && canJump && timeJump)
        {
            rb2D.AddForce(transform.up * thrust);
            canJump = false;
            timeJump = false;
            Invoke("ReloadTimeForJump", 1f);
        }

        if (strafeRight)
        {
            rb2D.velocity = new Vector2(speed * 25, rb2D.velocity.y);
            if (canJump)
            {
                animator.Play("PlayerWalkingRight");
            }
            else
            {
                animator.Play("PlayerStands");
            }
            strafeRight = false;
        }
        else if (strafeLeft)
        {
            rb2D.velocity = new Vector2(-speed * 25, rb2D.velocity.y);
            if (canJump)
            {
                animator.Play("PlayerWalkingLeft");
            }
            else
            {
                animator.Play("PlayerStands");
            }
            strafeLeft = false;
        }
        else
        {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
            animator.Play("PlayerStands");
        }
        doJump = false;
    }
}
