using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Hundimiento_plataforma : MonoBehaviour
{
    #region Variables
    Vector3 initial_pos;
    Rigidbody2D rb;
    public float gravedad_bajada;
    #endregion

    #region Guardar posicion inicial de la plataforma y gravedad a 0
    private void Start()
    {
        initial_pos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

        #region Hundimiento de la plataforma en el acido
        Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), GameObject.FindGameObjectWithTag("acido").GetComponent<CompositeCollider2D>(), true);
        #endregion
    }
    #endregion

    #region Personaje salta de la plataforma --> plataforma sube
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(transform.position != initial_pos)
        {
            rb.gravityScale = -1.5f;
        }
    }
    #endregion

    #region Si el personaje salta de la plataforma y cae en seguida
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb.gravityScale != 0f)
        {
            rb.gravityScale = 0f;
        }
    }
    #endregion

    #region Plataforma llega de nuevo a la posicion inicial
    private void Update()
    {
        if(transform.position.y > initial_pos.y)
        {
            rb.bodyType = RigidbodyType2D.Static;
            rb.gravityScale = 0;
            rb.bodyType = RigidbodyType2D.Dynamic;
            transform.position = initial_pos;
        }
    }
    #endregion

    
}
