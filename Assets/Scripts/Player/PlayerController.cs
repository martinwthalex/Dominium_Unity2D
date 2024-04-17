using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    #region Variables
    float x;
    Rigidbody2D rb;
    public float vel = 10;
    public float fuerza_salto = 18;
    bool canJump = true;
    SpriteRenderer sr;
    public static int vidas;
    public GameObject brazo;
    Animator anim;
    AudioSource src;
    public AudioClip[] steps;
    public AudioClip jump, jump_fall;
    bool deslizamiento = false;
    const float initial_fuerza_deslizamiento = 4;
    float current_fuerza_deslizamiento = initial_fuerza_deslizamiento;
    #endregion

    #region Singleton Management
    //Campo privado que referencia a esta instancia
    static PlayerController instance;

    /// <summary>
    /// Propiedad pública que devuelve una referencia a esta instancia
    /// Pertenece a la clase, no a esta instancia
    /// Proporciona un punto de acceso global a esta instancia
    /// </summary>
    public static PlayerController Instance
    {
        get { return instance; }
    }

    //Constructor
    void Awake()
    {
        //Asigna esta instancia al campo instance
        if (instance == null)
            instance = this;
        else
            Destroy(this);  //Garantiza que sólo haya una instancia de esta clase
    }
    #endregion

    void Start()
    {
        #region Inicializar Variables
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        x = Input.GetAxis("Horizontal");
        vidas = 8;
        anim = GetComponent<Animator>();
        Set_player_atributes();
        SetPlayerCanPlay(false);
        src = GetComponent<AudioSource>();
        //transform.position = new Vector3(1, 70, 0);
        #endregion
    }

    void Update()
    {
        Movement(vel);
        Jump();
        FootSteps();
        EasterEggs();
    }

    #region Easter Eggs Alex
    void EasterEggs()
    {
        if (Input.GetKey(KeyCode.F2)) Brazo_controller.Set_Can_Disparo_Plataformas(true);

    }
    #endregion

    #region Movimiento personaje
    void Movement(float vel)
    {
        x = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(x * vel, rb.velocity.y);
        if (x > 0)
        {
            sr.flipX = false;
            anim.SetBool("Run", true);
            Deslizamiento(deslizamiento);
        }
        if (x < 0)
        {
            sr.flipX = true;
            anim.SetBool("Run", true);
            Deslizamiento(deslizamiento);
        }
        if (x == 0)
        {
            anim.SetBool("Run", false);
            Deslizamiento(deslizamiento, true);
        }

    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.I) && canJump)
        {
            PlayClip(jump);
            rb.velocity = new Vector2(rb.velocity.x, fuerza_salto);
            canJump = false;
        }
    }
    #endregion

    #region Sonido Footsteps
    void FootSteps()
    {
        if(rb.velocity.x != 0)
        {
            if(!src.isPlaying && canJump)
            {
                int n = Random.Range(0, steps.Length);
                PlayClip(steps[n]);
            }
        }
    }
    #endregion

    #region Morir y Restar vidas
    public static void RestarVidas(int vidas_restar = 1)
    {
        vidas -= vidas_restar; 
        if (vidas <= 0)
        {
            Morir();
        }
    }
    static void Morir()
    {
        Scene escenaActual = SceneManager.GetActiveScene();
        SceneManager.LoadScene(escenaActual.name);
    }
    #endregion

    #region Colisiones
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("hielo"))
        {
            if(collision.GetContact(0).point.y < this.transform.position.y - 0.1f)// si se reduce de tamaño el personaje est evalor debe de cambiar
            {
                canJump = true;
                PlayClip(jump_fall);

            }
        }
        if (collision.gameObject.CompareTag("pinchos"))
        {
            Morir();
        }
        SetPlayerCanPlay(true);
    }
    #endregion

    #region Set Velocidad, fuerza de salto, anim vel, material inicial
    // Si al llamar a la funcion, no pones parametros estos se reinician por defecto
    public void Set_player_atributes(float vel_ = 10, float fuerza_salto_ = 18)
    {
        vel = vel_;
        fuerza_salto = fuerza_salto_;
        anim.SetFloat("Multiplier", 1);
    }
    #endregion

    #region Vel y Salto en Tunel de Viento
    public void TunelDeViento(bool tunel_de_viento, float divisor = 2)
    {
        if (tunel_de_viento)
        {
        vel /= divisor;
        fuerza_salto /= divisor;
        }
        else
        {
            Set_player_atributes();
        }
    }
    #endregion

    #region Get Flip X
    public bool Get_flipx()
    {
        return GetComponent<SpriteRenderer>().flipX;
    }
    #endregion

    #region Bloqueo Movimiento
    void SetPlayerCanPlay(bool _can_play)
    {
        this.enabled = _can_play;
    }
    #endregion

    #region Play Clip
    void PlayClip(AudioClip clip_)
    {
        //src.Stop(); 
        src.clip = clip_;
        src.Play();
    }
    #endregion

    // CONTINUAR POR AQUI, NO SE ACCEDE A LA VEL DE LOS FRAMES ASI
    #region Set Velocidad Frames Movimiento
    // Para adaptar la vel de la animacion en el tunel de viento
    public void SetVelFramesMov(bool tunel_de_viento, float multiplicador = 1)
    {
        if (tunel_de_viento)
        {
            anim.SetFloat("Multiplier", multiplicador);
        }
        else
        {
            Set_player_atributes();
        }
    }
    #endregion

    #region Baba Deslizante
    public void BabaDeslizante(float multiplicador_ = 1)
    {
        if (multiplicador_ != 1)
        {
            deslizamiento = true;
        }            
        else
        {
            deslizamiento = false;
        }
        vel *= multiplicador_;
        fuerza_salto *= multiplicador_;
    }

    void Deslizamiento(bool deslizamiento, bool parado = false)
    {
        if (deslizamiento)
        {
            if (Get_flipx())
            {                
                rb.AddForce(new Vector2(-current_fuerza_deslizamiento,0));
            }
            else
            {
                rb.AddForce(new Vector2(current_fuerza_deslizamiento, 0));
            }
            if (parado)
            {
                current_fuerza_deslizamiento -= Time.deltaTime;
                if(current_fuerza_deslizamiento <= 0)
                {
                    current_fuerza_deslizamiento = 0;
                }
            }
            else
            {
                current_fuerza_deslizamiento = initial_fuerza_deslizamiento;
            }
        }
    }
    #endregion

    #region Golpeo Sushi
    public void GolpeoSushi(bool flipeado, float fuerza)
    {
        Vector2 direccion;
        Vector2 desplaz_vertical = new Vector2(0,0);
        if (flipeado) direccion = Vector2.left;
        else direccion = Vector2.right;
        rb.AddForce(direccion * fuerza + desplaz_vertical);
    }
    #endregion
}
