using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limite_basicoEnem : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall")){
            Basico_enem.Set_limite(true);
        }
        if (collision.gameObject.CompareTag("Floor"))
        {
            Basico_enem.Set_limite(false);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor")){
            Basico_enem.Set_limite(true);
        }
    }
}
