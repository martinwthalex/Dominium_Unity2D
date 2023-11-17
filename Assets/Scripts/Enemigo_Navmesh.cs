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
    private bool objetivo_detectado = false;
    private Transform objetivo;
    private SpriteRenderer sprite;
    float distancia;
    int stopping_distance = 0;
    // Start is called before the first frame update
    void Start()
    {
        agente.updateRotation = false;
        agente.updateUpAxis = false;
        //print(puntosRuta[indiceRuta].position.x + "  " + puntosRuta[indiceRuta].position.y);
        sprite = this.GetComponent<SpriteRenderer>();
        agente.stoppingDistance = stopping_distance;
    }
    private void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        distancia = Vector3.Distance(personaje.position, this.transform.position);
        if(this.transform.position == puntosRuta[indiceRuta].position)
        {
            
            if(indiceRuta < puntosRuta.Length - 1)
            {
                indiceRuta++;
            }
            else if(indiceRuta == puntosRuta.Length - 1)
            {
                indiceRuta = 0;
                
            }
        }
        if (distancia < 9)
        {
            objetivo_detectado = true;
        }
        else
        {
            agente.stoppingDistance = stopping_distance;
        }
        //else objetivo_detectado = false;
        //poner un else para que vuelva a su posicion original si escapamos del enemigo
        Movimiento_enemigo(objetivo_detectado);
        Rotar_enemigo();
    }

    void Movimiento_enemigo(bool esDetectado)
    {
        if (esDetectado)
        {
            agente.SetDestination(personaje.position);
            objetivo = personaje;
            Mantener_distancia();
        }
        else
        {
            agente.SetDestination(puntosRuta[indiceRuta].position);
            objetivo = puntosRuta[indiceRuta];
        }
    }
    void Rotar_enemigo()
    {
        if(this.transform.position.x > objetivo.position.x)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;   
        }
    }
    void Mantener_distancia()
    {
        agente.stoppingDistance = 6;
    }
}
