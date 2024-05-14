using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dibujo_final : MonoBehaviour
{
    public Sprite v_2;
    public GameObject right_arrow;
    float timer = 5;

    private void Start()
    {
        right_arrow.SetActive(false);
    }

    private void Update()
    {

        if(timer < 0)
        {
            right_arrow.SetActive(true);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
    public void Cambiar_viñeta()
    {
        if (GetComponent<SpriteRenderer>().sprite != v_2)
        {
            GetComponent<SpriteRenderer>().sprite = v_2;
            right_arrow.SetActive(false);
            timer = 5;
        }
        else
        {
            SceneManager.LoadScene("MenuInicial");
        }
    }
}
