using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limite_manzana : MonoBehaviour
{
    public Transform manzana;
    float distancia = 1.5f;

    private void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("wall") || collision.gameObject.CompareTag("pinchos") || collision.gameObject.CompareTag("enemy"))
        {
            manzana.GetComponent<Manzana>().Set_limite(true);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            manzana.GetComponent<Manzana>().Set_limite(true);
        }
    }

    private void Update()
    {
        if (manzana != null)
        {
            if (this.manzana.GetComponent<Manzana>().Get_srFlip())
            {
                this.transform.position = manzana.position - new Vector3(distancia, 0f);
            }
            else
            {
                this.transform.position = manzana.position + new Vector3(distancia, 0f);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
