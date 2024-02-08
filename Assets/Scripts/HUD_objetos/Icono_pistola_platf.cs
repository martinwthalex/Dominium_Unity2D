using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icono_pistola_platf : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("bordecamara"))
        {
            Brazo_controller.Set_Can_Disparo_Plataformas(true);
            this.enabled = false;
            Destroy(this.gameObject);
        }
    }
}
