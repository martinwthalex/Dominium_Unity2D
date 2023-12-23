using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MenuOpciones : MonoBehaviour
{
    public Toggle pantallaCompleta; 

    public TMP_Dropdown dropdownCalidad, dropdownResolucion;
    public int calidad;

    Resolution[] resoluciones;


    [SerializeField] private AudioMixer audioMixer; //Sonido master


    public Slider brillo; //slider del brillo
    public float brilloValue; //valor del slider de brillo
    public Image panelBrillo; //Panel para cambiar el alpha y simular aumento o disminucion de brillo
    [SerializeField] TMP_Text porcentaje;

    void Start()
    {
        if (Screen.fullScreen)
        {
            this.pantallaCompleta.isOn = true;
        }
        else
        {
            this.pantallaCompleta.isOn = false;
        }

        calidad = PlayerPrefs.GetInt("numeroCalidad",2);
        dropdownCalidad.value = calidad;
        CambiarCalidad(calidad);

        RevisarResolucion();

        brillo.value = PlayerPrefs.GetFloat("brillo",0.3f); //si el slider de brillo no se le ha cambia el valor, x defecto vendra en 0.5
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, brillo.value);
        
    }

    void Update()
    {
        this.porcentaje.text = brilloValue.ToString("00")  + "%";
    }

    public void PantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

    public void CambiarVolumen(float volumen)
    {
        audioMixer.SetFloat("Volumen",volumen);
    }

    public void CambiarCalidad(int index)
    {
        QualitySettings.SetQualityLevel(index);
        PlayerPrefs.SetInt("numeroCalidad",dropdownCalidad.value);
        index = dropdownCalidad.value;
    }
    public void CambiarBrillo(float valor)
    {
        brilloValue = valor;
        PlayerPrefs.SetFloat("brillo",brilloValue);
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, brillo.value);
    }

    public void CambiarResolucion(int index)
    {
        PlayerPrefs.SetInt("numeroResolucion",dropdownResolucion.value);
        Resolution resolucion = resoluciones[index];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
    }

    public void RevisarResolucion()
    {
        resoluciones = Screen.resolutions; 
        dropdownResolucion.ClearOptions(); //borra la lista del dropdown y escribe en ella todas las resoluciones disponibles en el pc del usuario

        List<string> opciones = new List<string>();//crea lista nueva dentro del dropdown (esta vacio x lo q hay que rellenarlo)
        int resolucionActual = 0;

        for (int i = 0; i < resoluciones.Length; i++) //Rellena automaticamente el dropdown con las resoluciones que permita el pc del usuario
        {
            string opcion = resoluciones[i].width + "x" + resoluciones[i].height;
            opciones.Add(opcion);

            if (Screen.fullScreen && resoluciones[i].width == Screen.currentResolution.width && 
                resoluciones[i].height == Screen.currentResolution.height) //Revisa si la resolucion actual de la pantalla es la que esta seleccionada
            {
                resolucionActual = i;
            }
        }

        dropdownResolucion.AddOptions(opciones);
        dropdownResolucion.value = resolucionActual;
        dropdownResolucion.RefreshShownValue();

        dropdownResolucion.value = PlayerPrefs.GetInt("numeroResolucion",0);
    }


    public void Back()
    {
        if (Datos.escena == 1)
        {
            SceneManager.LoadScene("SampleScene");
        }
        else
        {
            SceneManager.LoadScene("MenuInicial");
        }
    }
}
