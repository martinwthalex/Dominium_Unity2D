using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo_Navmesh : MonoBehaviour
{
    public Transform personaje;
    private NavMeshAgent agente;
    public Transform[] puntosRuta;
    private int indiceRuta = 0;
    // Start is called before the first frame update
    void Start()
    {
        agente.updateRotation = false;
        agente.updateUpAxis = false;
        print(puntosRuta[indiceRuta].position.x + "  " + puntosRuta[indiceRuta].position.y);
    }
    private void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        if(this.transform.position == puntosRuta[indiceRuta].position)
        {
            print("eo");
            if(indiceRuta < puntosRuta.Length - 1)
            {
                indiceRuta++;
            }
            else if(indiceRuta == puntosRuta.Length - 1)
            {
                indiceRuta = 0;
                //print("reiniciando ruta");
            }
        }
        //print(indiceRuta);
        agente.SetDestination(puntosRuta[indiceRuta].position);
    }
}
