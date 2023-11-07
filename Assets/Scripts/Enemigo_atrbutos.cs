using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo_atrbutos : MonoBehaviour
{
    
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bala"))// si choca con la bala 
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))// si choca con el jugador
        {
            PlayerController.RestarVidas();
        }
    }
}
