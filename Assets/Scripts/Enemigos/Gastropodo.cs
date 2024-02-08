using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements.Experimental;

public class Gastropodo : MonoBehaviour
{
    public Transform personaje;
    private NavMeshAgent agente;
    public Transform[] puntosRuta;
    private int indiceRuta = 0;
    private bool objetivo_detectado = false;
    private Transform objetivo;
    private SpriteRenderer sr;
    float distancia;

    private void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        agente.updateRotation = false;
        agente.updateUpAxis = false;
        sr = this.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        distancia = Vector3.Distance(personaje.position, this.transform.position);
        if (this.transform.position == puntosRuta[indiceRuta].position)
        {

            if (indiceRuta < puntosRuta.Length - 1)
            {
                indiceRuta++;
            }
            else if (indiceRuta == puntosRuta.Length - 1)
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
            objetivo_detectado = false;
        }
        Movimiento_enemigo(objetivo_detectado);
        Rotar_enemigo();
    }

    void Movimiento_enemigo(bool esDetectado)
    {
        if (esDetectado)
        {
            agente.SetDestination(this.gameObject.transform.position);
        }
        else
        {
            agente.SetDestination(puntosRuta[indiceRuta].position);
            objetivo = puntosRuta[indiceRuta];
        }
    }
    void Rotar_enemigo()
    {
        if (this.transform.position.x > objetivo.position.x)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }
    void Disparo_acido()
    {

    }
}
