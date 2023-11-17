using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Brazo_controller : MonoBehaviour
{
    public GameObject jugador;
    private Quaternion originalArmRotation;
    private Vector3 originalArmPosition;
    private bool brazo_arriba = false, brazo_abajo = false;
    public float velocidadRotacion = 100f;
    public float escalaInicialX = 1f;



    void Start()
    {
        originalArmRotation = gameObject.transform.rotation;
        originalArmPosition = gameObject.transform.position;
        escalaInicialX = transform.localScale.x;
    }

    void Update()
    {
        Pulsar_varias_flechas();
        Vector3 direccion = ObtenerDireccionPrioritaria();
        RotarBrazo(direccion);
        FlipBrazo(direccion);
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
            float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
            Quaternion rotacionObjetivo = Quaternion.Euler(0f, 0f, angulo);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * velocidadRotacion);
        }
    }
    void FlipBrazo(Vector3 direccion)
    {
        // Verificar la dirección y ajustar la escala en consecuencia
        if (direccion.x < 0)
        {
            // Apuntando a la izquierda, flipear en el eje X
            transform.localScale = new Vector3(-escalaInicialX, transform.localScale.y, transform.localScale.z);
        }
        else if (direccion.x > 0)
        {
            // Apuntando a la derecha, restablecer la escala original
            transform.localScale = new Vector3(escalaInicialX, transform.localScale.y, transform.localScale.z);
        }
    }
}
