using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 
using UnityEngine;
using TMPro;

public class AgrandarTexto : MonoBehaviour //NO AGRANDA EL TEXTO, LOS CAMBIA DE COLOR AHORA XQ QUEDA MEJOR SALU2 SOY ALEX
{
    private TextMeshProUGUI textMeshPro;
    //private float originalSize;

    void Start()
    {
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        //originalSize = textMeshPro.fontSize;//Coje el tama�o de letra original del texto dnd haya sido colocado
    }

    public void OnPointerEnter()
    {
        //textMeshPro.fontSize = originalSize * 1.2f; // Agrandar el tama�o en un 20%

        textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, textMeshPro.color.a/2);
    }

    public void OnPointerExit()
    {
        //textMeshPro.fontSize = originalSize; // Volver al tama�o original

        textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, textMeshPro.color.a * 2);
    }
}