using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limite_basicoEnem : MonoBehaviour
{
    public Transform basico_enem;
    float distancia = 1.5f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            Basico_enem.Set_limite(true);
            
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            Basico_enem.Set_limite(true);
        }
    }

    private void Update()
    {
        if(basico_enem != null)
        {
            if (Basico_enem.Get_srFlip())
            {
                this.transform.position = basico_enem.position + new Vector3(distancia, 0f);
            }
            else
            {
                this.transform.position = basico_enem.position + new Vector3(-distancia, 0f);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
