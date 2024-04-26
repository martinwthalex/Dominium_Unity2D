using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston_aplasta : MonoBehaviour
{
    #region Variables
    Rigidbody2D rb;
    float timer = 0.5f;
    float vel_caida = 10;
    SpriteRenderer sr;
    Transform personaje;
    bool plat_subiendo = false;
    Vector3 pos_inicial;
    #endregion

    #region Inicializacion
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        pos_inicial = transform.position;

        personaje = PlayerController.Instance.gameObject.GetComponent<Transform>();
        if(personaje == null)
        {
            Debug.LogError("Personaje no encontrado");
        }
    }
    #endregion

    private void Update()
    {
        if(!plat_subiendo)
        {
            DetectarPersonaje();
        }
        else
        {
            PistonAplasta(false);
        }
    }

    #region Deteccion del personaje
    void DetectarPersonaje()
    {
        if ((personaje.position.x < transform.position.x + sr.size.x/2) && (personaje.position.x > transform.position.x - sr.size.x/2))
        {
            PistonAplasta(true);
        }        
    }
    #endregion

    #region Piston Aplasta!
    void PistonAplasta(bool aplasta)
    {
        if(aplasta)
        {
            timer -= Time.deltaTime;
            if(timer < 0)
            {
                transform.position -= Vector3.down * vel_caida * Time.deltaTime;
            }
        }
        else
        {
            transform.position += Vector3.up * vel_caida * Time.deltaTime;
            if(transform.position.y >= pos_inicial.y)
            {
                transform.position = pos_inicial;
                plat_subiendo = false;
            }
        }
    }
    #endregion

    #region Colisiones
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            plat_subiendo = true;
        }
    }
    #endregion
}
