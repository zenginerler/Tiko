using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private enum State {idle, running, jumping, falling}
    private State state = State.idle;
    private Collider2D coll;
    [SerializeField] private LayerMask ground;


    // setting variables
    public float movementSpeed = 5;
    public float jumpForce = 7;
    /*
    // better control settings
    public float speed = 10;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    */

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }


    // Update is called once per frame
    private void Update()
    {

        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        // basic player controls
        if (xAxis < 0) 
        {
            rb.velocity = new Vector2(-movementSpeed, rb.velocity.y);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (xAxis > 0)
        {
            rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            
        }
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            state = State.jumping;
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
        VelocityState();
        anim.SetInteger("state", (int)state);
    }

    private void VelocityState()
    {
        if (state == State.jumping)
        {
            if (rb.velocity.y < .1f){
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground)){
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > 3f){
            //Moving
            state = State.running;
        }
        else
        {
            state = State.idle;
        }

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
