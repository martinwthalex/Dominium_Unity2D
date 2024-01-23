using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicaOpciones : MonoBehaviour
{
    public GameObject panelOpciones;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MostrarPausa();
            Datos.escena = 1;
        }
    }

    public void MostrarPausa()
    {
        panelOpciones.SetActive(true);
        Time.timeScale = 0;
    }

    public void Reanudar()
    {
        Time.timeScale = 1;
    }

    public void ReloadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
