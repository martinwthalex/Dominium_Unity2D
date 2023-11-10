using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brazo_controller : MonoBehaviour
{
    public GameObject jugador;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //gameObject.transform.position = jugador.transform.position + new Vector3(0,0.7f,0);
            
            transform.eulerAngles.Set(0, 0, 180);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            //gameObject.transform.position = jugador.transform.position + new Vector3(0,-0.7f,0);
            
            transform.eulerAngles.Set(0, 0, -180);
        }
        //else
        //{
        //    gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        //}
    }
}
