using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton_puerta : MonoBehaviour
{
    public Sprite activado, desactivado;
    public GameObject puerta;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = desactivado;
    }

    #region Jugador pulsa boton
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().sprite = activado;
            puerta.SetActive(false);
        }
    }
    #endregion
}
