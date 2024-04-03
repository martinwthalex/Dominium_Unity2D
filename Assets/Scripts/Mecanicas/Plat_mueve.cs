using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Plat_mueve : MonoBehaviour
{
    NavMeshAgent agente;
    public Transform MAX_Y, MIN_Y;
    bool max = true;
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
        if (agente.remainingDistance == 0)
        {
            if (max)
            {
                agente.SetDestination(MIN_Y.transform.position);
                max = false;    
            }
            else
            {
                agente.SetDestination(MAX_Y.transform.position);
                max = true;
            }
        }

    }
}
