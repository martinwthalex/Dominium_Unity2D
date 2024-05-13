using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Sushi : MonoBehaviour
{
    #region Variables
    bool limite;
    Rigidbody2D rb;
    private Animator anim;
    public float vel;
    SpriteRenderer sr;
    float distancia;
    public Transform personaje;
    int vidas;
    GameObject hitmarker;
    public static bool hitmarker_destruido;
    [SerializeField] GameObject hitmarker_prefab;
    float rango_detect = 6f;
    float fuerza_golpeo = 2.5f;
    float timer = 2;
    bool stop_sushi = false;
    bool golpeo_sushi = false;

    bool en_pantalla = false;
    Vector3 viewportPosition;
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
        //anim.SetFloat("distance", rango_detect);
        personaje = GameObject.FindGameObjectWithTag("Player").transform;
        #endregion
    }
    private void Update()
    {
        if (en_pantalla)
        {
            #region Lógica de movimiento del sushi
            if(!stop_sushi)
            {
                if (personaje.position.y >= this.transform.position.y - (sr.size.y / 2) &&
                    personaje.position.y <= this.transform.position.y + (sr.size.y * 1.5))
                {
                    Calcular_distancia();
                }
                else
                {
                    Patrulla();
                }
            }
            #endregion

            #region Golpeo Sushi
            if(golpeo_sushi)
            {
                timer -= Time.deltaTime;
                if(timer < 1)
                {
                    golpeo_sushi = false;
                    timer = 2;
                }
                else
                {
                    PlayerController.Instance.GolpeoSushi(Get_srFlip(), fuerza_golpeo);
                }
            }
            #endregion
        }

        #region Occlusion Culling
        // Obtener la posición del objeto en coordenadas de vista de la pantalla
        viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        // Verificar si el objeto está dentro de la pantalla
        if (viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1)
        {
            en_pantalla = true;
        }
        else
        {
            en_pantalla = false;
        }
        #endregion
    }    

    #region Calcular Distancia
    void Calcular_distancia()
    {
        distancia = Vector2.Distance(this.transform.position, personaje.position);
        if(distancia  > rango_detect)
        {
            Patrulla();
        }
        else
        {
            if(!stop_sushi) Caza();
        }
    }
    #endregion

    #region Patrulla
    void Patrulla()
    {
        anim.SetBool("caza", false);
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
        anim.SetBool("caza", true);
        transform.position = Vector3.MoveTowards(this.transform.position, personaje.transform.position, vel * Time.deltaTime);
        
        // Flipear el enemigo dependiendo del personaje
        if (personaje.position.x > transform.position.x)
        {
            Set_srFlip(false);
        }
        else Set_srFlip(true);
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
        if (collision.gameObject.CompareTag("Player") && !stop_sushi)// si choca con el jugador
        {
            StartCoroutine(StopSushi());
            PlayerController.Instance.RestarVidas();
            CM_vcam1.Instance.AgitarCamara(true);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            golpeo_sushi = true;
        }

        if (collision.gameObject.CompareTag("acido"))
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    #region Stop Sushi
    IEnumerator StopSushi()
    {
        stop_sushi = true;
        anim.SetBool("caza", false);        
        yield return new WaitForSeconds(2);
        stop_sushi = false;
    }
    #endregion

}
