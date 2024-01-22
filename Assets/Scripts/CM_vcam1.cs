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
    ushort expected_tracked_object_offset_X;
    public static float initial_orthoSize;
    public static float orthoSize_value;
    float transition_vel_offset;
    public static float transition_vel_ortho;
    private void Start()
    {
        cam = this.GetComponent<CinemachineVirtualCamera>();
        transposer = cam.GetCinemachineComponent<CinemachineFramingTransposer>();
        initial_orthoSize = cam.m_Lens.OrthographicSize;
        orthoSize_value = 10f;
        cam.m_Lens.OrthographicSize = 3.48f;
        if (jugador == null) jugador = GameObject.FindGameObjectWithTag("Player");
        expected_tracked_object_offset_X = 3;
        transition_vel_offset = 4f;
        transition_vel_ortho = 1f;
    }
    private void Update()
    {
        if(!CameraChange.GetParkour())SetTrackedObjectOffset_X(expected_tracked_object_offset_X);
        else SetTrackedObjectOffset_X(0);
    }

    void SetTrackedObjectOffset_X(ushort value_expected)
    {
        if (transposer != null)
        {
            if (jugador != null)
            {
                float t = transition_vel_offset * Time.deltaTime;
                transposer.m_TrackedObjectOffset.x = Mathf.Lerp(GetCurrentTrackedObjectOffset_X(), (jugador.GetComponent<PlayerController>().Get_flipx() ? -value_expected : value_expected), t); ;
            }
            else Debug.LogError("Player not found");
        }
        
    }
    

    float GetCurrentTrackedObjectOffset_X()
    {
        return transposer.m_TrackedObjectOffset.x;
    }
    public static IEnumerator SetOrthoSize(float value, float initial_value)
    {
        float time_past = 0.0f;

        while (time_past < transition_vel_ortho)// 2--> tiempo que tarda la camara en llegar al maximo punto de zoom/deszoom
        {
            // Calcula la interpolación lineal entre el orthographic size inicial y el nuevo
            float t = time_past / transition_vel_ortho;
            cam.m_Lens.OrthographicSize = Mathf.Lerp(initial_value, value, t);

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

    public static float GetCurrentOrthoSize()
    {
        return cam.m_Lens.OrthographicSize;
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
