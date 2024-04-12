using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Sushi : MonoBehaviour
{
    #region Variables
    bool limite;
    Rigidbody2D rb;
    private Animator anim;
    int vel;
    SpriteRenderer sr;
    float distancia;
    public Transform personaje;
    int vidas;
    GameObject hitmarker;
    public static bool hitmarker_destruido;
    [SerializeField] GameObject hitmarker_prefab;
    float rango_detect = 6f;
    float timer = 0.14f;
    #endregion
    private void Start()
    {
        #region Inicializacion de Variables
        limite = false;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        vel = 5;
        vidas = 3;
        hitmarker_destruido = true;
        anim.SetFloat("distance", rango_detect);
        #endregion
    }
    private void Update()
    {
        #region Lógica de movimiento del sushi
        if (personaje.position.y >= this.transform.position.y - (sr.size.y / 2))
        {
            Calcular_distancia();
        }
        else
        {
            Patrulla();
        }
        #endregion
    }

    #region Calcular Distancia
    void Calcular_distancia()
    {
        anim.SetFloat("distance", distancia);
        distancia = Vector2.Distance(this.transform.position, personaje.position);
        if(distancia  > rango_detect)
        {
            Patrulla();
        }
        else
        {
            Caza();
        }
    }
    #endregion

    #region Patrulla
    void Patrulla()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        if (!limite)
        {
            if (!Get_srFlip())
            {
                rb.velocity += new Vector2(vel, 0);
            }
            else
            {
                rb.velocity -= new Vector2(vel, 0);
            }
        }
        else
        {
            if (Get_srFlip()) Set_srFlip(false);
            else Set_srFlip(true);
            limite = false;
        }
    }
    #endregion

    #region Caza
    void Caza()
    {        
        Vector3.MoveTowards(this.transform.position, personaje.transform.position, rango_detect);
    }
    #endregion

    #region Set Límite
    public void Set_limite(bool limite_)
    {
        limite = limite_;
    }
    #endregion

    #region Set y Get FlipX
    public bool Get_srFlip()
    {
        if (sr.flipX)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void Set_srFlip(bool flip)
    {
        sr.flipX = flip;
    }
    #endregion

    #region Colisiones del sushi
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bala"))// si choca con la bala 
        {
            Destroy(collision.gameObject);
            vidas--;
            if (hitmarker_destruido)
            {
                hitmarker = Instantiate(hitmarker_prefab, transform.position, Quaternion.identity);
                Hitmarker hitmarker_script = hitmarker.GetComponent<Hitmarker>();
                hitmarker_script.Inicializar_enemigo_pos(this.gameObject.transform);
                if (vidas <= 0)
                {
                    Destroy(hitmarker_script);
                    Destroy(gameObject);
                }
            }
        }
        if (collision.gameObject.CompareTag("Player"))// si choca con el jugador
        {
            
        }
    }
    #endregion
    
}
