using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy
{

    // FSM
    private enum State {idle, jumping, falling}
    private State state = State.idle;

    // Inspector variables
    [Header("Stats")]
    [SerializeField] private float leftBorder = 0;
    [SerializeField] private float rightBorder = 0;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float jumpLength = 3f;
    private bool facingLeft = true;
    public LayerMask ground;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    private void Update()
    {
        //Move();
        AnimationState();
        anim.SetInteger("state", (int)state);

    }


    private void Move()
    {
        if (facingLeft)
        {
            if (transform.position.x > leftBorder)
            {
                if (transform.localScale.x != 1 && coll.IsTouchingLayers(ground))
                {
                    transform.localScale = new Vector3(1, 1);
                }


                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                    state = State.jumping;
                }
            
            }

            else
            {

                facingLeft = false;
            }
        }

        else
        {
            if (transform.position.x < rightBorder)
            {
                if (transform.localScale.x != -1 && coll.IsTouchingLayers(ground))
                {
                    transform.localScale = new Vector3(-1,1);
                }

                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(jumpLength, jumpHeight);
                    state = State.jumping;
                }
            
            }
            else
            {
                facingLeft = true;
            }
        }


    }

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
        else
        {
            state = State.idle;
        }


    }




}   
