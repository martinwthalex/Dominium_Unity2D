using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hundimiento_plataforma : MonoBehaviour
{
    #region Variables
    Vector3 initial_pos;
    Rigidbody2D rb;
    #endregion

    #region Guardar posicion inicial de la plataforma y gravedad a 0
    private void Start()
    {
        initial_pos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }
    #endregion

    #region Personaje salta de la plataforma --> plataforma sube
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(transform.position != initial_pos)
        {
            rb.gravityScale = -0.5f;
        }
    }
    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(rb.gravityScale != 0f)
        {
            rb.gravityScale = 0f;
        }
    }

    #region Plataforma llega de nuevo a la posicion inicial
    private void Update()
    {
        if(transform.position.y > initial_pos.y)
        {
            rb.bodyType = RigidbodyType2D.Static;
            rb.gravityScale = 0;
            rb.bodyType = RigidbodyType2D.Dynamic;

        }
    }
    #endregion
}
