using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorPrueba : MonoBehaviour
{
    [SerializeField] float velocidad = 5, fuerzaSalto = 6;
    [SerializeField] bool suelo = false;
    [SerializeField] LayerMask mascara;
    Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


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
            brazo.transform.position = gameObject.transform.position + new Vector3(1.3f, -0.2f, 0);
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
    //void Update()
    //{
    //    if (Time.timeScale != 0)
    //    {
    //        MovimientoLateral();
    //        DetectarSuelo();
    //        Salto();
    //        //Disparar();
    //    }
    //}
    //void Disparar()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        // crear bala
    //    }
    //}
    //void MovimientoLateral()
    //{
    //    rb.velocity = new Vector2(0, rb.velocity.y);
    //    if (Input.GetKey(KeyCode.D))
    //        rb.velocity += new Vector2(velocidad, 0);
    //    if (Input.GetKey(KeyCode.A))
    //        rb.velocity += new Vector2(-velocidad, 0);
    //}
    //void DetectarSuelo()
    //{
    //    Vector3 origen = transform.position;
    //    Vector3 direccion = Vector3.down;
    //    float distancia = 0.2f;

    //    RaycastHit2D hit = Physics2D.Raycast(origen, direccion, distancia, mascara);
    //    Debug.DrawRay(origen, direccion * distancia, Color.red);

    //    suelo = false;
    //    if (hit) // Asegurarme de que el rayo ha golpeado en algo.
    //    {
    //        if (hit.collider.tag == "Suelo")
    //            suelo = true;
    //    }
    //}
    //void Salto()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space) && suelo)
    //    {
    //        rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
    //    }
    //}
}
