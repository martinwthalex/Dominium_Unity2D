using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Plat_mueve : MonoBehaviour
{
    NavMeshAgent agente;
    public Transform MAX_Y, MIN_Y;
    bool max = true;
    public float remainig;

    private void Awake()
    {
        ChangeOffsetPuntos();
    }

    private void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        agente.updateRotation = false;
        agente.updateUpAxis = false;

        if (MAX_Y == null || MIN_Y == null) Debug.LogError("No se ha encontrado la posición MAX_Y / MIN_Y");
        
        agente.SetDestination(MAX_Y.transform.position);
    }

    private void Update()
    {
        remainig = agente.remainingDistance;
        if (agente.remainingDistance == 0)
        {
            if (max)
            {
                agente.SetDestination(MIN_Y.transform.position);
                max = false;
                print("A");
            }
            else
            {
                agente.SetDestination(MAX_Y.transform.position);
                max = true;
                print("A");
            }
        }
    }

    #region MyRegion
    void ChangeOffsetPuntos()
    {
        if(Math.Abs(MAX_Y.position.x) < Math.Abs(MIN_Y.position.x) + 0.03f)
        {
            MAX_Y.position = new Vector3(MAX_Y.position.x + 0.03f, MAX_Y.position.y, MAX_Y.position.z);
        }
    }
    #endregion
}
