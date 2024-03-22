using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;
using Unity.VisualScripting;

public class CM_vcam1 : MonoBehaviour
{
    #region Singleton Management
    //Campo privado que referencia a esta instancia
    static CM_vcam1 instance;

    /// <summary>
    /// Propiedad p�blica que devuelve una referencia a esta instancia
    /// Pertenece a la clase, no a esta instancia
    /// Proporciona un punto de acceso global a esta instancia
    /// </summary>
    public static CM_vcam1 Instance
    {
        get { return instance; }
    }

    //Constructor
    void Awake()
    {
        //Asigna esta instancia al campo instance
        if (instance == null)
            instance = this;
        else
            Destroy(this);  //Garantiza que s�lo haya una instancia de esta clase
    }
    #endregion

    #region Variables
    static CinemachineVirtualCamera cam;
    [SerializeField] GameObject jugador;
    CinemachineFramingTransposer transposer;
    ushort expected_tracked_object_offset_X;
    public static float initial_orthoSize;
    public static float orthoSize_value;
    float transition_vel_offset;
    public static float transition_vel_ortho;
    #endregion

    #region Inicializar Variables
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
    #endregion

    #region Tracked Object Offset Manager
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

    #endregion

    #region OrthoSize Manager
    public static IEnumerator SetOrthoSize(float value, float initial_value)
    {
        float time_past = 0.0f;

        while (time_past < transition_vel_ortho)// 2--> tiempo que tarda la camara en llegar al maximo punto de zoom/deszoom
        {
            // Calcula la interpolaci�n lineal entre el orthographic size inicial y el nuevo
            float t = time_past / transition_vel_ortho;
            cam.m_Lens.OrthographicSize = Mathf.Lerp(initial_value, value, t);

            // Incrementa el tiempo pasado y espera un frame
            time_past += Time.deltaTime;
            yield return null;
        }

        // Aseg�rate de que el orthographic size sea exactamente el nuevo valor al finalizar la transici�n
        cam.m_Lens.OrthographicSize = value;

        // Puedes realizar acciones adicionales aqu� despu�s de que la transici�n haya concluido
        // Desactivar la l�gica de transici�n o realizar otras acciones
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
    #endregion

    public void AgitarCamara(bool agitar)
    {
        if (agitar)
        {
            //transposer.
        }

        else
        {

        }
    }

}
