using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    static bool parkour;
    static bool enable_camera_changes;
    private void Start()
    {
        parkour = false;
        enable_camera_changes = false;
    }

    private void Update()
    {
        if (this.gameObject.GetComponent<BoxCollider2D>().enabled == false && enable_camera_changes)
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = enable_camera_changes;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(!parkour) // zoom out
            {
                StartCoroutine(CM_vcam1.SetOrthoSize(CM_vcam1.orthoSize_value, CM_vcam1.GetCurrentOrthoSize()));
            }
            else //zoom in 
            {
                StartCoroutine(CM_vcam1.SetOrthoSize(CM_vcam1.initial_orthoSize, CM_vcam1.GetCurrentOrthoSize()));
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

    public static void EnableCameraChanges(bool _value = true)
    {
        enable_camera_changes = _value;
    }
}
