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
        
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)/* || (Input.GetKeyDown(KeyCode.RightArrow) && Input.GetKeyDown(KeyCode.UpArrow)) || (Input.GetKeyDown(KeyCode.LeftArrow) && Input.GetKeyDown(KeyCode.UpArrow))*/)
        {
            // Restablece la rotación del brazo a su posición original
            gameObject.transform.rotation = originalArmRotation;
            gameObject.transform.position = originalArmPosition;
            brazo_arriba = false;
            brazo_abajo = false;
        }
        if(brazo_abajo && Input.GetKeyDown(KeyCode.UpArrow)) gameObject.transform.rotation = originalArmRotation;
        else if(brazo_arriba && Input.GetKeyDown(KeyCode.DownArrow)) gameObject.transform.rotation = originalArmRotation;
        if (Input.GetKeyDown(KeyCode.UpArrow)&&gameObject.transform.rotation.z == 0)
        {
            brazo_arriba = true;
            // Rota el brazo -90 grados alrededor del eje Z
            if (jugador.GetComponent<PlayerController>().GetFlipX())
            {
                gameObject.transform.Rotate(Vector3.forward * -90f);
                gameObject.transform.position = jugador.transform.position + new Vector3(0.1f, 0.7f, 0);
            }
            else
            {
                gameObject.transform.Rotate(Vector3.forward * 90f);
                gameObject.transform.position = jugador.transform.position + new Vector3(-0.1f, 0.8f, 0);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && gameObject.transform.rotation.z == 0)
        {
            brazo_abajo = true;
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
