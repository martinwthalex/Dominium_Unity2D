using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public GameObject brazo;
    private Vector3 direccion;
    private float velocidad;
    
    void Update()
    {
        MoverBala();
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("wall") || collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("zonaplataforma"))
        {
            Destroy(gameObject);
        }
    }
    public void InicializarBala(Vector3 direccion, float velocidad)
    {
        if(direccion == Vector3.left)
        {
            Flip_bala();
        }
        else
        {
            Reiniciar_flip();
        }
        this.velocidad = velocidad;
        this.direccion = direccion;
    }

    void MoverBala()
    {
        transform.position += direccion * velocidad * Time.deltaTime;
    }
    void Flip_bala()
    {
        this.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    void Reiniciar_flip()
    {
        this.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
