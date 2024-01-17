using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;
using Unity.VisualScripting;

public class CM_vcam1 : MonoBehaviour
{
    static CinemachineVirtualCamera cam;
    [SerializeField] GameObject jugador;
    CinemachineFramingTransposer transposer;
    ushort tracked_object_offset_X;
    float initial_orthoSize;
    public static float orthoSize_value;
    private void Start()
    {
        cam = this.GetComponent<CinemachineVirtualCamera>();
        transposer = cam.GetCinemachineComponent<CinemachineFramingTransposer>();
        initial_orthoSize = cam.m_Lens.OrthographicSize;
        orthoSize_value = 10f;
        if (jugador == null) jugador = GameObject.FindGameObjectWithTag("Player");
        tracked_object_offset_X = 4;
        SetTrackedObjectOffset_X(tracked_object_offset_X);
    }
    private void Update()
    {
        SetTrackedObjectOffset_X(tracked_object_offset_X);
    }

    void SetTrackedObjectOffset_X(ushort value)
    {
        if (transposer != null)
        {
            if (jugador != null)
            {
                if (jugador.GetComponent<PlayerController>().Get_flipx())
                {
                    transposer.m_TrackedObjectOffset.x = -value;
                }
                else
                {
                    transposer.m_TrackedObjectOffset.x = value;
                }
            }
            else Debug.LogError("Player not found");
        }
    }
    public static void SetOrthoSize(float value)
    {
        cam.m_Lens.OrthographicSize = value;
    }
}
