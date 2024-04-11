using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limite_sushi : MonoBehaviour
{
    #region Variables
    public Transform sushi;
    float distancia = 1.5f;
    #endregion

    #region Trigger Enter y Exit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("wall") || collision.gameObject.CompareTag("pinchos") || collision.gameObject.CompareTag("enemy"))
        {
            sushi.GetComponent<Sushi>().Set_limite(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            sushi.GetComponent<Sushi>().Set_limite(true);
        }
    }
    #endregion

    private void Update()
    {
        #region Colocación del límite
        if (sushi != null)
        {
            if (this.sushi.GetComponent<Sushi>().Get_srFlip())
            {
                this.transform.position = sushi.position - new Vector3(distancia, 0f);
            }
            else
            {
                this.transform.position = sushi.position + new Vector3(distancia, 0f);
            }
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion
    }
}
