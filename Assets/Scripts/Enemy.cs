using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    // Start() variables
    protected Rigidbody2D rb;
    protected Animator anim;
    protected Collider2D coll;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JumpedOn()
    {   
        anim.SetTrigger("death");
        rb.velocity = Vector2.zero;
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }


}
