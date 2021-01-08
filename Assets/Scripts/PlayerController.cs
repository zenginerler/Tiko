using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start() variables
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;

    // FSM
    private enum State {idle, running, jumping, falling, hurt}
    private State state = State.idle;

    // Inspector variables
    public LayerMask ground;
    public Text CollectableCount;
    public AudioSource cherryFX;
    public AudioSource footstep;
    [SerializeField] private float speed = 6.5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float hurtForce = 8.5f;
    [SerializeField] private int cherries = 0;

    /*
    // better control settings
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
        if (state != State.hurt)
        {
            Movement();
        }

        AnimationState();
        anim.SetInteger("state", (int)state);
    }

    // Handles general control inputs of the user
    private void Movement()
    {
        // basic player controls
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        if (xAxis < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (xAxis > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            Jump();
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

    //Handles jump input exclusively
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        state = State.jumping;
    }

    // Changes animation states according to the player
    private void AnimationState()
    {
        if (state == State.jumping)
        {
            if (rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) <= 3f){
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > 3f)
        {
            //Moving
            state = State.running;
        }
        else
        {
            state = State.idle;
        }

    }

    //Handles cherry collection
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectable")
        {
            cherries += 1;
            cherryFX.Play();
            CollectableCount.text = cherries.ToString();
            Destroy(collision.gameObject);
        }
    }

    //Handles interactions with enemies
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (state == State.falling)
            {
                enemy.JumpedOn();
                Jump();
            }
            else
            {
                state = State.hurt;

                if (other.gameObject.transform.position.x > transform.position.x){
                    // Enemy is to my right therefore I should be damaged and move left
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);

                }
                else{
                    // Enemy is to my left therefore I should be damaged and move right
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }
        }
    }

    //plays footstep sound
    private void Footstep()
    {
        footstep.Play();
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
