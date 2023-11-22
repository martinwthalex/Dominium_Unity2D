using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Brazo_controller : MonoBehaviour
{
    public Transform jugador, cañon;
    
    
    public float velocidadRotacion = 100f;
    public float escalaInicialX;
    public GameObject balaPrefab;
    public float velocidadBala = 30f;
    float angulo;
    GameObject bala;
    Vector3 direccion;
    bool izquierda, derecha, arriba, abajo;

    void Start()
    {
        
        escalaInicialX = Mathf.Abs(transform.localScale.x);
        
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
        if (Input.GetKeyDown(KeyCode.X))
        {
            
            Disparar();
            
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

}
