using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;
using Unity.VisualScripting;

public class CM_vcam1 : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    [SerializeField] GameObject jugador;
    CinemachineFramingTransposer transposer;
    private void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        transposer = cam.GetCinemachineComponent<CinemachineFramingTransposer>();
        if(transposer != null)
        {
            if (!jugador.GetComponent<SpriteRenderer>().flipX)
            {
                transposer.m_ScreenX = 0.6f;
                transposer.m_TrackedObjectOffset.x = 2f;
            }
            else
            {
                transposer.m_ScreenX = 0.4f;
                transposer.m_TrackedObjectOffset.x = -2f;
            }
        }
        else
        {
            print("NO");
        }
        
    }
    private void Update()
    {
        if (transposer != null)
        {
            if (!jugador.GetComponent<SpriteRenderer>().flipX)
            {
                transposer.m_ScreenX = 0.6f;
                transposer.m_TrackedObjectOffset.x = 2f;
            }
            else
            {
                transposer.m_ScreenX = 0.4f;
                transposer.m_TrackedObjectOffset.x = -2f;
            }
        }
    }
}
