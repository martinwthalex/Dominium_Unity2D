using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float vel = 20;
    [SerializeField] float fuerza_salto = 15;
    bool canJump = true;
    SpriteRenderer sr;
    public static int vidas = 0;
    public GameObject brazo;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();


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
        rb.velocity = new Vector2(0, rb.velocity.y);
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity += new Vector2(vel, 0);
            sr.flipX = false;
            brazo.GetComponent<SpriteRenderer>().flipX = false;
            
            // NO TOCAR ESTOS VALORES
            brazo.transform.position = gameObject.transform.position + new Vector3(1.3f,-0.2f,0);
        }
            
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity += new Vector2(-vel, 0);
            sr.flipX = true;
            brazo.GetComponent<SpriteRenderer>().flipX = true;
            
            // NO TOCAR ESTOS VALORES
            brazo.transform.position = gameObject.transform.position + new Vector3(-1.3f, -0.2f, 0);
            
        }

    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {

            //rb.AddForce(new Vector2(0,fuerza_salto));
            //print("saltando");
            rb.velocity = new Vector2(rb.velocity.x, fuerza_salto);
            canJump = false;
            //rb.AddForce(Vector2.up * fuerza_salto, ForceMode2D.Impulse);

        }

    }
    
    public static void RestarVidas()
    {
        vidas--;
        if (vidas < 0)
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
        if(collision.gameObject.CompareTag("Floor"))
        {
            canJump = true;
            //print("suelo");
        }
        
    }
    public bool GetFlipX()
    {
        return sr.flipX;
    }
}
