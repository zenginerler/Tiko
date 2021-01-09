using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start() variables
    protected Rigidbody2D rb;
    protected Animator anim;
    protected Collider2D coll;
    protected AudioSource deathFX;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        deathFX = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    public void JumpedOn()
    {   
        anim.SetTrigger("death");
        deathFX.Play();
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Collider2D>().enabled = false;
    }


    private void Death()
    {
        Destroy(this.gameObject);
    }


}
