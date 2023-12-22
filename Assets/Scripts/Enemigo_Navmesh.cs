using Cinemachine.Utility;
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
    private bool chasing;
    private Transform objetivo;
    private SpriteRenderer sr;
    float distancia;
    int stopping_distance = 0;
    float velocidad_inicial;
    float aceleracion_inicial;
    private Animator animator;
    float timer;
    float tiempo_volver_perseguir;
    public GameObject bubble;
    [SerializeField] GameObject bubble_prefab;
    bool bubble_creada = false;
    public bubble_enem_pulmon bubble_Enem_Pulmon_ = null;
    Transform transform_inicial;
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
        transform_inicial = agente.gameObject.transform;
        timer = 2f;
        chasing = false;
        tiempo_volver_perseguir = 0.8f;
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
        if (!chasing)
        {
            if (distancia < 9)
            {
                objetivo_detectado = true;
                chasing = true;
            }
            else
            {
                agente.stoppingDistance = stopping_distance;
                objetivo_detectado = false;
                
            }
        }
        else
        {
            objetivo_detectado = true;
        }
        
        Movimiento_enemigo(objetivo_detectado);
        Rotar_enemigo();
        
    }

    void Movimiento_enemigo(bool esDetectado)
    {
        if (esDetectado)
        {
            if (!bubble_creada)
            {
                agente.SetDestination(personaje.position);
                agente.speed = velocidad_inicial * 1.5f;
                agente.acceleration = aceleracion_inicial * 1.5f;
                objetivo = personaje;
            }
            else
            {
                if (bubble_enem_pulmon.Get_player_inBubble())
                {
                    agente.SetDestination(this.gameObject.transform.position);
                    agente.speed = velocidad_inicial * 0;
                    agente.acceleration = aceleracion_inicial * 0;
                    agente.angularSpeed = 0f;
                    objetivo = this.gameObject.transform;
                }
                else
                {
                    Delete_bubble();
                    tiempo_volver_perseguir -= Time.deltaTime;
                    if(tiempo_volver_perseguir <= 0)
                    {
                        agente.SetDestination(personaje.position);
                        agente.speed = velocidad_inicial * 1.5f;
                        agente.acceleration = aceleracion_inicial * 1.5f;
                        objetivo = personaje;
                        tiempo_volver_perseguir = 0.8f;
                    }
                    
                }

            }
            
            Mantener_distancia();
        }
        else
        {
            agente.gameObject.transform.rotation = transform_inicial.rotation;
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
        agente.stoppingDistance = 4;
        Recolocar_enemigo();
        if (objetivo == personaje && agente.remainingDistance < 2.5f)
        {
            if (!bubble_creada)
            {
                Crear_bubble();
            }
            Atacar(true);

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                //print("Distancia:  " + agente.remainingDistance + "\nObjetivo: " + agente.destination.ToString() + "\nAgente: " + this.gameObject);
                PlayerController.RestarVidas();
                timer = 2f;
            }
            
        }
        else if(objetivo == this.gameObject.transform)
        {
            Atacar(true);

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                //print("Distancia:  " + agente.remainingDistance + "\nObjetivo: " + agente.destination.ToString() + "\nAgente: " + this.gameObject);
                PlayerController.RestarVidas();
                timer = 2f;
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
        Atacar(false);
        
    }
    void Recolocar_enemigo()// MOVE TOWARDS HACE QUE AUNQUE EL DESTINATION SEA ÉL MISMO, SE MUEVA HACIA EL JUGADOR --> ARREGLAR 
    {
        if (!bubble_creada)
        {
            // Dentro del script del enemigo
            Vector3 directionToPlayer = personaje.position - transform.position;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

            // Redondear el ángulo a múltiplos de 90 grados
            float roundedAngle = Mathf.Round(angle / 90) * 90;

            // Calcular la rotación basada en el ángulo redondeado
            Quaternion targetRotation = Quaternion.Euler(0, 0, roundedAngle);

            // Calcular la posición alrededor del jugador
            Vector3 offset = new Vector3(5f, 0f, 0.0f);  // Ajusta el offset según sea necesario
            Vector3 rotatedOffset = targetRotation * offset;
            Vector3 targetPosition = personaje.position + rotatedOffset;

            // Mover el enemigo hacia la posición alrededor del jugador
            float moveSpeed = 5f;  // Ajusta la velocidad de movimiento según sea necesario
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Aplicar la rotación al enemigo
            float rotationSpeed = 10f;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        }
    }
}
