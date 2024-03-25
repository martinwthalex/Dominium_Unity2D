using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baba_desliza : MonoBehaviour
{
    float multiplicador = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.BabaDeslizante(multiplicador);
            PlayerController.Instance.SetVelFramesMov(true, multiplicador);
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Se reinicia el material del player
            PlayerController.Instance.Set_player_atributes();
        }       
    }
}
