using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cascada : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bala"))
        {
            Brazo_controller.Set_Can_Disparo_Plataformas(true);
            Destroy(gameObject);
        }
    }
}
