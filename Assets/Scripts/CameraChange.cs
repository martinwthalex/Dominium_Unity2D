using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    static bool parkour;
    private void Start()
    {
        parkour = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(!parkour)
            {
                StartCoroutine(CM_vcam1.SetOrthoSize(CM_vcam1.orthoSize_value, CM_vcam1.initial_orthoSize));
            }
            else
            {
                StartCoroutine(CM_vcam1.SetOrthoSize(CM_vcam1.initial_orthoSize, CM_vcam1.orthoSize_value));
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    
    public static void SetParkour(bool _parkour)
    {
        parkour = _parkour;
    }
    public static bool GetParkour()
    {
        return parkour;
    }
}
