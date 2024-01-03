using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    
    public GameObject brazo;
    private Vector3 direccion;
    private float velocidad;
    
   
    // Update is called once per frame
    void Update()
    {
        MoverBala();
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("wall") || collision.gameObject.CompareTag("Floor"))
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
        //transform.Translate(new Vector3(direccion.x,direccion.y,0f) * velocidad * Time.deltaTime);
        transform.position += direccion * velocidad * Time.deltaTime;
        // Puedes agregar lógica para destruir la bala cuando está fuera de la pantalla u otras condiciones.
    }
    void Flip_bala()
    {
        this.transform.localScale = new Vector3(-0.2f, transform.localScale.y, transform.localScale.z);
    }
    void Reiniciar_flip()
    {
        this.transform.localScale = new Vector3(0.2f, transform.localScale.y, transform.localScale.z);
    }
}
