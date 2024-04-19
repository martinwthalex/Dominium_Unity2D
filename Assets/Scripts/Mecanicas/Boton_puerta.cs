using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton_puerta : MonoBehaviour
{
    #region Jugador pulsa boton
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
    #endregion
}
