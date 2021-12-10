using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Texture2D tex;

    public Rigidbody2D rb2D;
    public Sprite mySprite;
    public SpriteRenderer sr;
    public float thrust = 1f;
    public float speed = 1f;

    protected bool canJump = false;
    protected bool timeJump = true;
    protected bool strafeLeft = false;
    protected bool strafeRight = false;
    protected bool doJump = false;


    // Start is called before the first frame update
    void Start()
    {
        mySprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, 25.0f, 56.0f), new Vector2(0.5f, 0.5f), 100.0f);

        sr.color = new Color(0.9f, 0.9f, 0.5f, 1.0f);
        sr.sprite = mySprite;
        transform.position = new Vector3(0.0f, -2.0f, 0.0f);
    }

    protected void ReloadTimeForJump()
    {
        timeJump = true;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Blocks"){
            canJump = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Blocks")
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
            Invoke("ReloadTimeForJump", 0.5f);
        }
        if (strafeLeft || strafeRight)
        {
            transform.position += new Vector3(speed, 0, 0) * Input.GetAxis("Horizontal");
            strafeLeft = false;
            strafeRight = false;
        }
        // Alternatively, specify the force mode, which is ForceMode2D.Force by default
        //rb2D.AddForce(transform.up * thrust, ForceMode2D.Impulse);
        doJump = false;
    }
}
