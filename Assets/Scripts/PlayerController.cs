using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    float vel = 15;
    bool canJump = true;
    SpriteRenderer sr;
    public static int vidas = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement(vel);
        Jump();
        if (vidas < 0)
        {
            Morir();
        }
        //print(onFloor);
    }

    void Movement(float vel)
    {
        rb.velocity = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity += new Vector2(vel, 0);
            sr.flipX = false;
            
        }
            
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity += new Vector2(-vel, 0);
            sr.flipX = true;
            //gameObject.GetComponent<BulletSpawn>().spawn.transform.position -= new Vector3(-6f, 0, 0);
        }

        //if (Input.GetKey(KeyCode.UpArrow))
        //{
            
        //    sr.flipX = true;
        //}
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            canJump = false;
            rb.AddForce(new Vector2(0,120f));
            
        }

    }
    public static void RestarVidas()
    {
        vidas--;
        
    }
    void Morir()
    {
        Destroy(this);
        Destroy(gameObject);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Floor")
        {
            canJump = true;
        }
    }
}
