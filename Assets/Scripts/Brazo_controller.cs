//using System.Collections;
//using System.Collections.Generic;
//using Unity.Burst.Intrinsics;
//using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Brazo_controller : MonoBehaviour
{
    public Transform jugador, cañon;
    public float timer_sobrecalentamiento = 2;
    
    private float tiempoUltimoDisparo = 0f;
    public float intervaloEntreDisparos = 0.5f; // Ajusta este valor según tus necesidades
   
    public float velocidadRotacion = 100f;
    public float escalaInicialX;
    public GameObject balaPrefab;
    public GameObject hieloPrefab;
    public float velocidadBala = 30f, velocidadHielo = 30f;
    float angulo;
    GameObject bala, hielo;
    private GameObject[] hielos_creados;
    private int MAX_hielos = 4;
    private int contador = -1;
    Vector3 direccion;
    bool izquierda, derecha, arriba, abajo;
    public static bool disparo_plataformas;
    const int sobrecalentamiento = 30;
    
    int balas_disparadas = 0;
    bool can_disparar = true;
    Animator anim;

    void Start()
    {
        hielos_creados = new GameObject[MAX_hielos];
        escalaInicialX = Mathf.Abs(transform.localScale.x);
        derecha = true;
        Set_Can_Disparo_Plataformas(false);
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            izquierda = true;
            derecha = false;
            arriba = false; abajo = false;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            izquierda = false;
            derecha = true;
            arriba = false; abajo = false;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            izquierda = false;
            derecha = false;
            arriba = true; abajo = false;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            izquierda = false;
            derecha = false;
            arriba = false; abajo = true;
        }
        
        direccion = ObtenerDireccionPrioritaria();
        RotarBrazo(direccion);
        
        SeguirProtagonista();
        if (Input.GetKeyDown(KeyCode.X) && can_disparar)
        {
            balas_disparadas++;
            if(balas_disparadas <= sobrecalentamiento)
            {
                //anim.SetBool("Sobrecalentamiento", false);
                Disparar();
                //print("PUM!");
            }
            else 
            {
                float tiempoActual = Time.time;
                float tiempoDesdeUltimoDisparo = tiempoActual - tiempoUltimoDisparo;
                if (tiempoDesdeUltimoDisparo <= intervaloEntreDisparos)
                {
                    Sobrecalentamiento_arma();
                    //print("CALOR");
                }
                else
                {
                    Disparar();
                    
                }
                tiempoUltimoDisparo = tiempoActual;
               
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Z) && derecha && disparo_plataformas || Input.GetKeyDown(KeyCode.Z) && izquierda && disparo_plataformas)
        {
            Disparo_plataformas();
            
        }
        if (!can_disparar)
        {
            Sobrecalentamiento_arma();
        }
    }
    
    

    Vector3 ObtenerDireccionPrioritaria()
    {
        bool izquierda = Input.GetKey(KeyCode.LeftArrow);
        bool derecha = Input.GetKey(KeyCode.RightArrow);
        bool arriba = Input.GetKey(KeyCode.UpArrow);
        bool abajo = Input.GetKey(KeyCode.DownArrow);

        if (arriba && (izquierda || derecha))
        {
            return Vector3.up;
        }
        else if (abajo && (izquierda || derecha))
        {
            return Vector3.down;
        }
        else if (izquierda && (arriba || abajo))
        {
            return Vector3.left;
        }
        else if (derecha && (arriba || abajo))
        {
            return Vector3.right;
        }

        if (izquierda) return Vector3.left;
        if (derecha) return Vector3.right;
        if (arriba) return Vector3.up;
        if (abajo) return Vector3.down;

        return Vector3.zero;
    }
    

    void RotarBrazo(Vector3 direccion)
    {
        if (direccion != Vector3.zero)
        {
            if (direccion == Vector3.left)
            {
                FlipBrazo();
                angulo = 360f;
                transform.rotation = Quaternion.Euler(0f, 0f, angulo);
            }
            else
            {
                Reiniciar_flip();
                angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, angulo);
            }
            
            
        }
    }
    float Rotacion_bala(Vector3 direccion)
    {
        if (direccion != Vector3.zero)
        {
            if (direccion == Vector3.left)
            {
                
                angulo = 360f;
                return angulo;
            }
            else
            {
                
                angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
                return angulo;
            }
        }
        else
        {
            angulo = 0f;
            return angulo;
        }
    }
    void FlipBrazo()
    {
        this.transform.localScale = new Vector3(-escalaInicialX, transform.localScale.y, transform.localScale.z);
    }
    void Reiniciar_flip()
    {
        transform.localScale = new Vector3(escalaInicialX, transform.localScale.y, transform.localScale.z);
    }
    void SeguirProtagonista()
    {
        // Mantener el brazo en el centro del protagonista
        transform.position = jugador.position;
    }
    void Disparar()
    {
        Vector3 direccionDisparo;
        if (derecha)
        {
            direccionDisparo = Vector3.right;
        }
        else if (izquierda)
        {
            direccionDisparo = Vector3.left;
        }
        else if (arriba)
        {
            direccionDisparo = Vector3.up;
        }
        else
        {
            direccionDisparo = Vector3.down;
        }
        float angulo_disparo = Rotacion_bala(direccionDisparo);
       
        Quaternion rot = Quaternion.Euler(0f, 0f, angulo_disparo);
        bala = Instantiate(balaPrefab, cañon.position, rot);
        BulletMovement scriptBala = bala.GetComponent<BulletMovement>();
        scriptBala.InicializarBala(direccionDisparo, velocidadBala);
    }
    void Disparo_plataformas()
    {
        Vector3 direccionDisparo = Vector3.right;
        if (derecha)
        {
            direccionDisparo = Vector3.right;
        }
        else if (izquierda)
        {
            direccionDisparo = Vector3.left;
        }
        Quaternion rot = Quaternion.Euler(0f, 0f, 0);
        hielo = Instantiate(hieloPrefab, cañon.position, rot);
        contador++;
        if(contador >= MAX_hielos)
        {
            int n = 0;
            Destroy(hielos_creados[0]);
            hielos_creados[n] = null;
            while(n < MAX_hielos)
            {
                if(n != (MAX_hielos -1))hielos_creados[n] = hielos_creados[n + 1];
                else hielos_creados[n] = hielo;
                n++;
            }
            contador--;
        }
        else
        {
            hielos_creados[contador] = hielo;
        }
        
        Hielo scriptHielo = hielo.GetComponent<Hielo>();
        scriptHielo.InicializarHielo(direccionDisparo, velocidadHielo);
    }
    public static void Set_Can_Disparo_Plataformas(bool can)
    {
        disparo_plataformas = can;
    }
    public static bool Get_Can_Disparo_Plataformas()
    {
        return disparo_plataformas;
    }
    void Sobrecalentamiento_arma()
    {
        anim.SetBool("Sobrecalentamiento", true);
        can_disparar = false;
        timer_sobrecalentamiento -= Time.deltaTime;
        if(timer_sobrecalentamiento <= 0)
        {
            balas_disparadas = 0;
            can_disparar = true;
            timer_sobrecalentamiento = 2;
            anim.SetBool("Sobrecalentamiento", false);
        }
    }
}
