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
    public GameObject bubble;
    [SerializeField] GameObject bubble_prefab;
    bool bubble_creada = false;
    public bubble_enem_pulmon bubble_Enem_Pulmon_ = null;

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
            objetivo_detectado = false;
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
            if (!bubble_creada)
            {
                Crear_bubble();
            }
            
            agente.speed = velocidad_inicial * 2;
            agente.acceleration = aceleracion_inicial * 2;
            agente.SetDestination(personaje.position);
            objetivo = personaje;
            Mantener_distancia();
        }
        else
        {
            Delete_bubble();
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
    void Crear_bubble()
    {
        bubble = Instantiate(bubble_prefab, this.transform.position, Quaternion.identity);
        bubble_Enem_Pulmon_ = bubble.GetComponent<bubble_enem_pulmon>();
        bubble_Enem_Pulmon_.Inicializar_bubble_pos(this.transform);
        bubble_creada = true;
    }
    public void Delete_bubble()
    {
        Destroy(bubble);
        bubble_creada = false;
    }
    
}
