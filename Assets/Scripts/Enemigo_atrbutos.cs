using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo_atrbutos : MonoBehaviour
{
   
    int vidas;
    GameObject hitmarker;
    [SerializeField] GameObject hitmarker_prefab;
    private void Start()
    {
        
        vidas = 2;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bala"))// si choca con la bala 
        {
            
            Destroy(collision.gameObject);
            vidas--;
            hitmarker = Instantiate(hitmarker_prefab,transform.position,Quaternion.identity);
            Hitmarker hitmarker_script = hitmarker.GetComponent<Hitmarker>();
            hitmarker_script.Inicializar_enemigo_pos(this.gameObject.transform);
            if (vidas <= 0)
            {
                Destroy(hitmarker);
                Destroy(hitmarker_script);
                Destroy(gameObject);
            }
            
        }
        else if (collision.gameObject.CompareTag("Player"))// si choca con el jugador
        {
            PlayerController.RestarVidas();
        }
    }
    
}
