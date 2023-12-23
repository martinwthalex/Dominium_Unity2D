using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial2 : MonoBehaviour
{
    private void Start()
    {
        Datos.escena = 0;
    }
    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Datos.escena = 1;
        //SceneManager.LoadScene("MenuInicial");
    }
    public void Opciones()
    {
        SceneManager.LoadScene("MenuOpciones");
    }

    public void Salir()
    {
        Debug.Log("Exit....");
        Application.Quit();
    }
}
