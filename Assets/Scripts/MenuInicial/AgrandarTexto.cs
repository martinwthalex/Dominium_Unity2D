using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AgrandarTexto : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    private float originalSize;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        originalSize = textMeshPro.fontSize;//Coje el tama�o de letra original del texto dnd haya sido colocado
    }

    public void OnPointerEnter()
    {
        textMeshPro.fontSize = originalSize * 1.2f; // Agrandar el tama�o en un 20%
    }

    public void OnPointerExit()
    {
        textMeshPro.fontSize = originalSize; // Volver al tama�o original
    }
}