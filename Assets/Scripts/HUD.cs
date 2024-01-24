using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] TMP_Text texto_arma;
    [SerializeField] GameObject panel_arma;
    
    private void Start()
    {
        Set_panel_arma(true,"J to fire");
        
    }
    private void Update()
    {
        if (Brazo_controller.Get_Can_Disparo_Plataformas())
        {
            Set_panel_arma(true, "L to create new platform");
        }
    }
    private void Set_panel_arma(bool panel_setActive,string _texto)
    {
        panel_arma.SetActive(panel_setActive);
        texto_arma.text = _texto;
    }

    
}
