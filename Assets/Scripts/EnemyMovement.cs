using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
    public Transform jugador;
    float distancia;
    public Vector3 puntoInicial;
    Animator animator;
    SpriteRenderer sr;

    private void Start()
    {
        animator = GetComponent<Animator>();
        puntoInicial = transform.position;
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        distancia = Vector2.Distance(transform.position, jugador.position);
        animator.SetFloat("Distancia", distancia);
    }
    public void Girar(Vector3 objetivo)
    {
        if(transform.position.x  < objetivo.x)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX=false;
        }
    }

}
