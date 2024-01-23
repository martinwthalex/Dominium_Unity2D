using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    static float vel = 10;
     static float fuerza_salto = 18;
    bool canJump = true;
    SpriteRenderer sr;
    public static int vidas;
    public GameObject brazo;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        vidas = 8;
        Set_player_atributes();
        anim = GetComponent<Animator>();
        SetPlayerCanPlay(false);
        transform.position = new Vector3(1, 70, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //print(canJump);
        Movement(vel);
        Jump();
        
        //Jugador_cayendo();
    }

    void Movement(float vel)
    {
        float x = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(x * vel, rb.velocity.y);
        if (x > 0)
        {
            //rb.velocity += new Vector2(vel, 0);
            sr.flipX = false;
            anim.SetBool("Run",true);
        }
            
        if (x < 0)
        {
           // rb.velocity += new Vector2(-vel, 0);
            sr.flipX = true;
            anim.SetBool("Run", true);
        }
        if(x == 0)
        {
            anim.SetBool("Run", false);
        }
        

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            sr.flipX = false;
        }
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            //sr.flipX = false;
            
            rb.velocity = new Vector2(rb.velocity.x, fuerza_salto);
            canJump = false;
            
        }

    }
    
    public static void RestarVidas()
    {
        vidas--; 
        if (vidas <= 0)
        {
            Morir();
        }
    }
    static void Morir()
    {
        //Destroy(this);
        //Destroy(gameObject);
        Scene escenaActual = SceneManager.GetActiveScene();
        SceneManager.LoadScene(escenaActual.name);
        

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("hielo"))
        {
            if(collision.GetContact(0).point.y < this.transform.position.y - 0.1f)// si se reduce de tamaño el personaje est evalor debe de cambiar
            {
                canJump = true;
            }
        }
        if (collision.gameObject.CompareTag("pinchos"))
        {
            Morir();
        }
        SetPlayerCanPlay(true);
    }
    public static void Set_player_atributes(float vel_ = 10, float fuerza_salto_ = 18)
    {
        vel = vel_;
        fuerza_salto = fuerza_salto_;
    }
    public bool Get_flipx()
    {
        return GetComponent<SpriteRenderer>().flipX;
    }

    void SetPlayerCanPlay(bool _can_play)
    {
        this.enabled = _can_play;
    }
}
