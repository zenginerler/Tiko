using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    
    /*
    // better control settings
    public float speed = 10;
    public float jumpForce = 9;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    */

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }



    // Update is called once per frame
    private void Update()
    {

        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        // basic player controls
        if (xAxis < 0) 
        {
            rb.velocity = new Vector2(-5, rb.velocity.y);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            anim.SetBool("running", true);
        }
        else if (xAxis > 0)
        {
            rb.velocity = new Vector2(5, rb.velocity.y);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            anim.SetBool("running", true);
        }
        else
        {
            anim.SetBool("running", false);
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = new Vector2(rb.velocity.x, 5);
        }


        /*
        // better controls from "mix & jam"
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y);

        Walk(dir);

        if (Input.GetButtonDown("Jump"))
        {
            Jump(Vector2.up);
        }

        // adds additional gravity to make the jumping feel little bit better. Inspired by "Board to Bits Games"
        // works with "better controls" not with the basic controls
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        */

    }
    /*
    // required functions to make better controls work 
    private void Walk(Vector2 dir)
    {
        rb.velocity = (new Vector2(dir.x * speed, rb.velocity.y));
    }
    private void Jump(Vector2 dir)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;
    }
    */

}
