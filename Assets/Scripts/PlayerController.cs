using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    float vel = 15, jumpForce = 20;
    bool onFloor = false;
    public LayerMask mascara;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement(vel);
        Jump();
        DetectFloor();
        //print(onFloor);
    }

    void Movement(float vel)
    {
        rb.velocity = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.RightArrow))
            rb.velocity += new Vector2(vel, 0);
        if (Input.GetKey(KeyCode.LeftArrow))
            rb.velocity += new Vector2(-vel, 0);
        
    }
    void Jump()
    {
        if (Input.GetKey(KeyCode.UpArrow) && onFloor)
        {
            //print("salto");
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            
        }

    }
    void DetectFloor()
    {
        // Hay que crear un origen, una direccion y una distancia
        Vector3 origen = transform.position;
        Vector3 direccion = Vector3.down;
        float distancia = 0.2f;

        RaycastHit2D hit = Physics2D.Raycast(origen, direccion, distancia,mascara); // mascara es donde solo se va a fijar, en unity se pueden seleccionar las capas 
                                                                                     // ignorará todos los colliders que estén fuera de la capa que le indiquemos




        Debug.DrawRay(origen, direccion * distancia, Color.red);
        onFloor = false;
        if (hit)// si esa variable de tipo raycast tiene información , es decir si ha colisionado con algo
        {
           // Debug.Log("tag: "+ hit.collider.tag);
            if (hit.collider.tag == "Suelo") // el tag del objeto con el que ha colisionado
            {
                onFloor = true;
            }
        }

    }
    //private bool OnFloor()
    //{

    //    Vector3 origen = transform.position;
    //    Vector3 direccion = Vector3.down;
    //    float distancia = 0.2f;

    //    RaycastHit2D hit = Physics2D.Raycast(origen, direccion, distancia);
    //    Debug.DrawRay(origen, direccion * distancia, Color.red);

    //    onFloor = false;
    //    if (hit) // Asegurarme de que el rayo ha golpeado en algo.
    //    {
    //        //print("hit");
    //        if (hit.collider.tag == "Floor")
    //        {
    //            onFloor = true;
    //            //print("floor");
    //        }

    //    }
    //    else onFloor = false;
    //    return onFloor;
    //}
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Floor"))
    //    {
    //        print("suelo");
    //        //Digo que no está saltando (para que pueda volver a saltar)
    //        onFloor = true;
            
    //        //Le quito la fuerza de salto remanente que tuviera
    //        rb.velocity = new Vector2(rb.velocity.x, 0);
    //    }
    //    else onFloor = false;
    //}
}
