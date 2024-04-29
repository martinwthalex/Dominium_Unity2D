using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston_aplasta : MonoBehaviour
{
    #region Variables
    public float vel_caida = 5;
    const float timer = 0.5f;
    float current_timer;
    SpriteRenderer sr;
    Transform personaje;
    Vector3 pos_inicial;
    bool aplasta = false;
    bool stop = false;

    public float vel_agitacion = 60f; //how fast it shakes
    public float cantidad_agitacion = 0.02f; //how much it shakes
    bool shaking = true;

    #endregion

    #region Inicializacion
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        pos_inicial = transform.position;

        personaje = PlayerController.Instance.gameObject.GetComponent<Transform>();
        if(personaje == null)
        {
            Debug.LogError("Personaje no encontrado");
        }

        current_timer = timer;

    }
    #endregion

    #region Logica Piston (Update)
    private void Update()
    {        
        if (aplasta)
        {
            PistonAplasta(true);
        }
        else
        {
            if(transform.position != pos_inicial && !shaking)
            {
                current_timer -= Time.deltaTime;
                if(current_timer <= 0)
                {
                    stop = false;
                    PistonAplasta(false);
                }
                else stop = true;
            }
            else
            {
                ShakePiston(true);
                current_timer = timer;
                DetectarPersonaje();
            }
        }
    }
    #endregion

    #region Deteccion del personaje
    void DetectarPersonaje()
    {
        if ((personaje.position.x < transform.position.x + sr.size.x/*/1.5*/) && (personaje.position.x > transform.position.x - sr.size.x/*/1.5*/))
        {
            aplasta = true;
            ShakePiston(false);
        }        
    }
    #endregion

    #region Piston Aplasta!
    void PistonAplasta(bool aplasta)
    {
        if(aplasta)
        {            
            if(!stop) transform.position += Time.deltaTime * vel_caida * Vector3.down;            
        }
        else
        {
            if(transform.position.y >= pos_inicial.y)
            {
                transform.position = pos_inicial;
            }
            else
            {
                if (!stop) transform.position += Time.deltaTime * vel_caida * Vector3.up;
            }
        }
    }
    #endregion

    #region Colisiones
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Player"))
        {
            aplasta = false;
            #region Sonido piston toca suelo
            GetComponent<AudioSource>().Play();
            #endregion
        }        

        #region Comprobar si el jugador está debajo y no en el lado
        if (collision.contacts.Length > 0)
        {
            ContactPoint2D contact = collision.contacts[0];
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.5)
            {
                if (collision.gameObject.CompareTag("Player"))
                {
                    PlayerController.Morir();
                }
            }
        }
        #endregion
    }
    #endregion

    #region Shake Plataforma
    void ShakePiston(bool shake)
    {
        if (shake)
        {
            shaking = true;
            gameObject.transform.position = new Vector3((pos_inicial.x + Mathf.Sin(Time.time * vel_agitacion) * cantidad_agitacion),
            pos_inicial.y + (Mathf.Sin(Time.time * vel_agitacion) * cantidad_agitacion), 0);
        }
        else
        {
            shaking = false;
        }
    }
    #endregion
}
