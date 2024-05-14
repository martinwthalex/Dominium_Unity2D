using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicaOpciones : MonoBehaviour
{
    public GameObject panelOpciones;
    bool pausa = false;

    private void Start()
    {
        panelOpciones.SetActive(false);
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Reanudar();
            Datos.escena = 1;
        }

    }

    public void Reanudar()
    {
        if (pausa)
        {
            Time.timeScale = 1;
            panelOpciones.SetActive(false);
            pausa = false;
        }
        else
        {
            Time.timeScale = 0;
            panelOpciones.SetActive(true);
            pausa = true;            
        }
    }    

    public void ExitMenu()
    {        
        SceneManager.LoadScene("MenuInicial");
    }

    public void RestartScene()
    {
        PlayerController.Instance.Morir();
        Reanudar();
    }
}
