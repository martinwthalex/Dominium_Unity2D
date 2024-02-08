using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Manzana : MonoBehaviour
{
    bool limite;
    Rigidbody2D rb;
    private Animator anim;
    int vel;
    SpriteRenderer sr;
    Vector2 distancia;
    public Transform personaje;
    private NavMeshAgent agente;
    bool patrulla;
    int vidas;
    GameObject hitmarker;
    public static bool hitmarker_destruido;
    [SerializeField] GameObject hitmarker_prefab;
    float timer;
    private void Start()
    {
        limite = false;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        agente = GetComponent<NavMeshAgent>();
        vel = 6;
        vidas = 3;
        patrulla = true;
        agente.updateRotation = false;
        agente.updateUpAxis = false;
        agente.enabled = false;
        hitmarker_destruido = true;
        timer = 3f;
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
        distancia.x = Vector2.Distance(this.transform.position, personaje.position);
        anim.SetFloat("distance", distancia.x);
        if (distancia.x < 5f)
        {
            float vertical_offset = 0.5f;
            if (personaje.position.y > this.transform.position.y + vertical_offset)
            {
                timer -= Time.deltaTime;
                if(timer < 0)
                {
                    patrulla = true;
                    timer = 3f;
                }
            }
            else patrulla = false;

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
    void Caza()
    {
        agente.enabled = true;
        this.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        agente.SetDestination(personaje.position);
        Rotar();
    }
    public void Set_limite(bool limite_)
    {
        limite = limite_;
    }

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
        
    }

}
