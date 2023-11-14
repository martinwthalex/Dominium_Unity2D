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
    void Start()
    {
        originalArmRotation = gameObject.transform.rotation;
        originalArmPosition = gameObject.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)){
            print("Brazo_abajo = " + brazo_abajo);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            print("Brazo_arriba = " + brazo_arriba);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Restablece la rotación del brazo a su posición original
            gameObject.transform.rotation = originalArmRotation;
            gameObject.transform.position = originalArmPosition;
            brazo_arriba = false;
            brazo_abajo = false;
        }
        if(brazo_abajo && Input.GetKeyDown(KeyCode.UpArrow))
        {
            gameObject.transform.rotation = originalArmRotation;
            
        }
        else if(brazo_arriba && Input.GetKeyDown(KeyCode.DownArrow))
        {
            gameObject.transform.rotation = originalArmRotation;
            
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)&&!Get_Brazo_Arriba())
        {
            brazo_arriba = true;
            brazo_abajo=false;
            // Rota el brazo -90 grados alrededor del eje Z
            if (jugador.GetComponent<PlayerController>().GetFlipX())
            {
                gameObject.transform.Rotate(Vector3.forward * -90f);
                gameObject.transform.position = jugador.transform.position + new Vector3(0.1f, 0.5f, 0);
            }
            else
            {
                gameObject.transform.Rotate(Vector3.forward * 90f);
                gameObject.transform.position = jugador.transform.position + new Vector3(-0.1f, 0.8f, 0);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !Get_Brazo_Abajo())
        {
            brazo_abajo = true;
            brazo_arriba = false;
            // Rota el brazo -90 grados alrededor del eje Z
            if (jugador.GetComponent<PlayerController>().GetFlipX())
            {
                gameObject.transform.Rotate(Vector3.forward * 90f);
                gameObject.transform.position = jugador.transform.position + new Vector3(-0.2f, -1.4f, 0);
            }
            else
            {
                gameObject.transform.Rotate(Vector3.forward * -90f);
                gameObject.transform.position = jugador.transform.position + new Vector3(0.2f, -1.4f, 0);
            }
            
        }
    }
    
    public bool Get_Brazo_Arriba()
    {
        return brazo_arriba;
    }
    public bool Get_Brazo_Abajo()
    {
        return brazo_abajo;
    }
}
