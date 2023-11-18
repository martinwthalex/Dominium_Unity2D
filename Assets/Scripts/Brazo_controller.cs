using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Brazo_controller : MonoBehaviour
{
    public Transform jugador, cañon;
    private Quaternion originalArmRotation;
    private Vector3 originalArmPosition;
    private bool brazo_arriba = false, brazo_abajo = false;
    public float velocidadRotacion = 100f;
    public float escalaInicialX = 1f;
    public GameObject balaPrefab;
    public float velocidadBala = 30f;

    void Start()
    {
        originalArmRotation = gameObject.transform.rotation;
        originalArmPosition = gameObject.transform.position;
        escalaInicialX = Mathf.Abs(transform.localScale.x);
        
    }

    void Update()
    {
        Pulsar_varias_flechas();
        Vector3 direccion = ObtenerDireccionPrioritaria();
        RotarBrazo(direccion);
        
        Reiniciar_flip(direccion);
        SeguirProtagonista();
        if (Input.GetKeyDown(KeyCode.X))
        {
            Disparar();
        }
        //if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    // Restablece la rotación del brazo a su posición original
        //    gameObject.transform.rotation = originalArmRotation;
        //    gameObject.transform.position = originalArmPosition;
        //    brazo_arriba = false;
        //    brazo_abajo = false;
        //}
        //if(brazo_abajo && Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    gameObject.transform.rotation = originalArmRotation;

        //}
        //else if(brazo_arriba && Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    gameObject.transform.rotation = originalArmRotation;

        //}
        //if (Input.GetKeyDown(KeyCode.UpArrow)&&!Get_Brazo_Arriba())
        //{
        //    brazo_arriba = true;
        //    brazo_abajo=false;
        //    // Rota el brazo -90 grados alrededor del eje Z
        //    if (jugador.GetComponent<PlayerController>().GetFlipX())
        //    {
        //        gameObject.transform.Rotate(Vector3.forward * -90f);
        //        gameObject.transform.position = jugador.transform.position + new Vector3(0.1f, 0.5f, 0);
        //    }
        //    else
        //    {
        //        gameObject.transform.Rotate(Vector3.forward * 90f);
        //        gameObject.transform.position = jugador.transform.position + new Vector3(-0.1f, 0.8f, 0);
        //    }
        //}
        //if (Input.GetKeyDown(KeyCode.DownArrow) && !Get_Brazo_Abajo())
        //{
        //    brazo_abajo = true;
        //    brazo_arriba = false;
        //    // Rota el brazo -90 grados alrededor del eje Z
        //    if (jugador.GetComponent<PlayerController>().GetFlipX())
        //    {
        //        gameObject.transform.Rotate(Vector3.forward * 90f);
        //        gameObject.transform.position = jugador.transform.position + new Vector3(-0.2f, -1.4f, 0);
        //    }
        //    else
        //    {
        //        gameObject.transform.Rotate(Vector3.forward * -90f);
        //        gameObject.transform.position = jugador.transform.position + new Vector3(0.2f, -1.4f, 0);
        //    }

        //}
    }
    void Pulsar_varias_flechas()
    {
        
    }
    public bool Get_Brazo_Arriba()
    {
        return brazo_arriba;
    }
    public bool Get_Brazo_Abajo()
    {
        return brazo_abajo;
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
                FlipBrazo(direccion);
            }
            else
            {
                float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
                //print(angulo);

                transform.rotation = Quaternion.Euler(0f, 0f, angulo);
            }
            
            
        }
    }
    void FlipBrazo(Vector3 direccion)
    {
        
        // Verificar la dirección y ajustar la escala en consecuencia
        if (direccion.x < 0)
        {
            // Apuntando a la izquierda, flipear en el eje X
            //print("escala negativa");
            this.transform.localScale = new Vector3(-escalaInicialX, transform.localScale.y, transform.localScale.z);
            //transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (direccion.x > 0)
        {
            // Apuntando a la derecha, restablecer la escala original
            //print("escala positiva");
            transform.localScale = new Vector3(escalaInicialX, transform.localScale.y, transform.localScale.z);
        }
        
    }
    void Reiniciar_flip(Vector3 direccion)
    {
        if(direccion == Vector3.up || direccion == Vector3.down || direccion == Vector3.right)
        {
            transform.localScale = new Vector3(escalaInicialX, transform.localScale.y, transform.localScale.z);
        }
    }
    void SeguirProtagonista()
    {
        // Mantener el brazo en el centro del protagonista
        transform.position = jugador.position;
    }
    void Disparar()
    {
        Vector3 direccionDisparo = ObtenerDireccionDisparo();
        Quaternion rotacionDisparo = ObtenerRotacionDisparo();

        GameObject bala = Instantiate(balaPrefab, cañon.position, rotacionDisparo);
        BulletMovement scriptBala = bala.GetComponent<BulletMovement>();
        scriptBala.InicializarBala(direccionDisparo, velocidadBala);
    }

    Vector3 ObtenerDireccionDisparo()
    {
        // Obtener la dirección en el espacio mundial a lo largo del eje derecho local del brazo
        Vector3 direccionLocal = Vector3.right;
        Vector3 direccionMundial = transform.TransformDirection(direccionLocal).normalized;
        print(direccionMundial.x);
        // Ajustar la escala de la bala según la dirección local del brazo
        if (direccionMundial.x < 0)
        {
            print("flipeando bala");
            balaPrefab.transform.localScale = new Vector3(-Mathf.Abs(balaPrefab.transform.localScale.x), balaPrefab.transform.localScale.y, balaPrefab.transform.localScale.z);
        }
        else
        {
            //print("NO");
            balaPrefab.transform.localScale = new Vector3(Mathf.Abs(balaPrefab.transform.localScale.x), balaPrefab.transform.localScale.y, balaPrefab.transform.localScale.z);
        }

        return direccionMundial;
    }

    Quaternion ObtenerRotacionDisparo()
    {
        // Obtener el ángulo de rotación en grados
        float anguloRotacion = Mathf.Atan2(transform.right.y, transform.right.x) * Mathf.Rad2Deg;

        // Ajustar la rotación de la bala según el ángulo de rotación del brazo
        Quaternion rotacionDisparo = Quaternion.Euler(0f, 0f, anguloRotacion);

        return rotacionDisparo;
    }
}
