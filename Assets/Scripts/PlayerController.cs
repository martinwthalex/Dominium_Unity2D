using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    float vel = 15;
    [SerializeField] float fuerza_salto = 120;
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
        //Jugador_cayendo();
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
            
        }

    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            canJump = false;
            rb.AddForce(new Vector2(0,fuerza_salto));
            //rb.velocity += new Vector2(0, fuerza_salto);
            //rb.AddForce(Vector2.up * fuerza_salto, ForceMode2D.Impulse);

        }

    }
    //void Jugador_cayendo()
    //{
    //    //if (canJump == false)
    //    //{
    //    //    float timer = 0.05f;
    //    //    timer -= Time.deltaTime;
    //    //    if(timer < 0)
    //    //    {
    //    //        rb.gravityScale = 90f;
    //    //        print("CAYENDO");
    //    //    }
    //    //}
    //    //else
    //    //{
    //    //    rb.gravityScale = 8f;
    //    //}
    //}
    public static void RestarVidas()
    {
        vidas--;
        
    }
    void Morir()
    {
        SceneManager.LoadScene("SampleScene");
        //Destroy(this);
        //Destroy(gameObject);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Floor")
        {
            canJump = true;
        }
    }
}
