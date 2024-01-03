using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Timeline;


public class Basico_enem : MonoBehaviour
{
    static bool limite;
    Rigidbody2D rb;
    int vel;
    static SpriteRenderer sr;
    float distancia;
    public Transform personaje;
    private NavMeshAgent agente;
    bool patrulla;
    int vidas;
    GameObject hitmarker;
    public static bool hitmarker_destruido;
    [SerializeField] GameObject hitmarker_prefab;
    private void Start()
    {
        limite = false;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        agente = GetComponent<NavMeshAgent>();
        vel = 5;
        vidas = 2;
        patrulla = true;
        agente.updateRotation = false;
        agente.updateUpAxis = false;
        agente.enabled = false;
        hitmarker_destruido = true;
    }
    private void Update()
    {
        if (patrulla)
        {
            Patrulla();
        }
        else
        {
            Caza();
        }
        Calcular_distancia();
    }
    void Calcular_distancia()
    {
        distancia = Vector2.Distance(this.transform.position, personaje.position);
        if (distancia < 7f)
        {
            if (this.transform.position.y > personaje.position.y)
            {
                patrulla = false;
            }
            else
            {
                patrulla = true;
            }
        }
        else
        {
            patrulla = true;
        }
    }
    void Patrulla()
    {
        agente.enabled = false;
        rb.velocity = new Vector2(0, rb.velocity.y);
        if (!limite)
        {
            if (Get_srFlip())
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
    void Caza()
    {
        agente.enabled = true;
        this.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        agente.SetDestination(personaje.position);
        Rotar();
    }
    public static void Set_limite(bool limite_)
    {
        limite = limite_;
    }

    public static bool Get_srFlip()
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
    void Rotar()
    {
        if (this.transform.position.x > personaje.position.x)
        {
            Set_srFlip(false);
        }
        else
        {
            Set_srFlip(true);
        }
    }
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
        else if (collision.gameObject.CompareTag("Player"))// si choca con el jugador
        {
            PlayerController.RestarVidas();
        }
    }
}