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
    private SpriteRenderer sr;
    float distancia;
    int stopping_distance = 0;
    float velocidad_inicial;
    float aceleracion_inicial;
    private Animator animator;
    float timer = 1f;

    // Start is called before the first frame update
    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        agente.updateRotation = false;
        agente.updateUpAxis = false;
        //print(puntosRuta[indiceRuta].position.x + "  " + puntosRuta[indiceRuta].position.y);
        sr = this.GetComponent<SpriteRenderer>();
        agente.stoppingDistance = stopping_distance;
        velocidad_inicial = agente.speed;
        aceleracion_inicial = agente.acceleration;
        animator = this.GetComponent<Animator>();
        
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
            agente.speed = velocidad_inicial * 2;
            agente.acceleration = aceleracion_inicial * 2;
            agente.SetDestination(personaje.position);
            objetivo = personaje;
            Mantener_distancia();
        }
        else
        {
            agente.speed = velocidad_inicial;
            agente.acceleration = aceleracion_inicial;
           
            agente.SetDestination(puntosRuta[indiceRuta].position);
            objetivo = puntosRuta[indiceRuta];
        }
    }
    void Rotar_enemigo()
    {
        if(this.transform.position.x > objetivo.position.x)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;   
        }
    }
    void Mantener_distancia()
    {
        agente.stoppingDistance = 6;
        if(agente.remainingDistance < 6)
        {
            Atacar(true);
            
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                PlayerController.RestarVidas();
                timer = 1f;
            }
        }
        else
        {
            Atacar(false);
        }
    }
    void Atacar(bool ataque)
    {
        animator.SetBool("Ataque", ataque);
    }
    
}
