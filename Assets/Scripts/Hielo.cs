using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hielo : MonoBehaviour
{
    
    private Vector3 direccion;
    private float velocidad;
    private Rigidbody2D rb;
    bool rb_static = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
    void Update()
    {
        if (!rb_static)
        {
            MoverHielo();
        }
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall") || collision.gameObject.CompareTag("hielo"))
        {
            rb_static = true;
            rb.bodyType = RigidbodyType2D.Static;
        }
           
        if (collision.gameObject.CompareTag("Floor"))
        {
            Destroy(this.gameObject);
        }

    }
    public void InicializarHielo(Vector3 direccion, float velocidad)
    {
        this.velocidad = velocidad;
        this.direccion = direccion;
    }

    void MoverHielo()
    {

        if(direccion == Vector3.right)
        {
            transform.position += direccion * velocidad * Time.deltaTime;
        }
        else if(direccion == Vector3.left)
        {
            transform.position += direccion * velocidad * Time.deltaTime;
        }
        
        // Puedes agregar lógica para destruir la bala cuando está fuera de la pantalla u otras condiciones.
    }
    
}
