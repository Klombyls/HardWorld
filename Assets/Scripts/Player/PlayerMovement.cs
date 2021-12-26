using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public HealthBar health;
    public Inventory inv;
    private Animator animator;
    public Rigidbody2D rb2D;
    public float thrust = 1f;
    public float speed = 1f;
    public float y1;

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
            int distanceBlocks = (int)(y1 - transform.position.y) / 3;
            if(distanceBlocks > 3)
            {
                health.TakingDamageFromFall((float)distanceBlocks * 4);
            }  
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
        y1 = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("a"))
        {
            rb2D.velocity = new Vector2(-speed * 25, rb2D.velocity.y);
            switch (inv.idArmor)
            {
                case 10:
                    animator.Play("PlayerWithIronArmorWalkingLeft");
                    break;
                case 11:
                    animator.Play("PlayerWithGoldenArmorWalkingLeft");
                    break;
                case 12:
                    animator.Play("PlayerWithBossArmorWalkingLeft");
                    break;
                default:
                    animator.Play("PlayerWalkingLeft");
                    break;
            }
        }
        else if (Input.GetKey("d"))
        {
            rb2D.velocity = new Vector2(speed * 25, rb2D.velocity.y);
            switch (inv.idArmor)
            {
                case 10:
                    animator.Play("PlayerWithIronArmorWalkingRight");
                    break;
                case 11:
                    animator.Play("PlayerWithGoldenArmorWalkingRight");
                    break;
                case 12:
                    animator.Play("PlayerWithBossArmorWalkingRight");
                    break;
                default:
                    animator.Play("PlayerWalkingRight");
                    break;
            }
        }
        else
        {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
            switch (inv.idArmor)
            {
                case 10:
                    animator.Play("PlayerWithIronArmorStands");
                    break;
                case 11:
                    animator.Play("PlayerWithGoldenArmorStands");
                    break;
                case 12:
                    animator.Play("PlayerWithBossArmorStands");
                    break;
                default:
                    animator.Play("PlayerStands");
                    break;
            }
        }
        if (Input.GetKey("w"))
        {
            doJump = true;
        }
    }

    private void FixedUpdate()
    {
        if (doJump && canJump && timeJump)
        {
            rb2D.AddForce(transform.up * thrust);
            canJump = false;
            timeJump = false;
            Invoke("ReloadTimeForJump", 1f);
        }
        doJump = false;
    }
}
