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
    public static float initial_orthoSize;
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
    public static IEnumerator SetOrthoSize(float value)
    {
        float time_past = 0.0f;

        while (time_past < 2)
        {
            // Calcula la interpolación lineal entre el orthographic size inicial y el nuevo
            float t = time_past / 2;
            cam.m_Lens.OrthographicSize = Mathf.Lerp(initial_orthoSize, value, t);

            // Incrementa el tiempo pasado y espera un frame
            time_past += Time.deltaTime;
            yield return null;
        }

        // Asegúrate de que el orthographic size sea exactamente el nuevo valor al finalizar la transición
        cam.m_Lens.OrthographicSize = value;

        // Puedes realizar acciones adicionales aquí después de que la transición haya concluido
        // Desactivar la lógica de transición o realizar otras acciones
        DisableTransitionLogic();
    }

    private static void DisableTransitionLogic()
    {
        if (CameraChange.GetParkour())
        {
            CameraChange.SetParkour(false);
        }
        else
        {
            CameraChange.SetParkour(true);
        }
    }
}
