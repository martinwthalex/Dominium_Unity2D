using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 
using UnityEngine;
using TMPro;

public class AgrandarTexto : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    private float originalSize;

    void Start()
    {
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        originalSize = textMeshPro.fontSize;//Coje el tamaño de letra original del texto dnd haya sido colocado
    }

    public void OnPointerEnter()
    {
        textMeshPro.fontSize = originalSize * 1.2f; // Agrandar el tamaño en un 20%
    }

    public void OnPointerExit()
    {
        textMeshPro.fontSize = originalSize; // Volver al tamaño original
    }
}