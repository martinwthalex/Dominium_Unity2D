using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] GameObject[] protas_image;
    [SerializeField] TMP_Text texto_arma;
    [SerializeField] GameObject panel_arma;
    
    private void Start()
    {
        Set_panel_arma(true,"X to fire");
        Set_protas_image(PlayerController.vidas);
        
    }
    private void Update()
    {
        Set_protas_image(PlayerController.vidas);
        if (Brazo_controller.Get_Can_Disparo_Plataformas())
        {
            Set_panel_arma(true, "Z to create new platform");
        }
    }
    private void Set_panel_arma(bool panel_setActive,string _texto)
    {
        panel_arma.SetActive(panel_setActive);
        texto_arma.text = _texto;
    }

    public void Set_protas_image(int _vidas)
    {
        
        if(_vidas < protas_image.Length) protas_image[_vidas].SetActive(false);
        else
        {
            protas_image[0].SetActive(true);
            protas_image[1].SetActive(true);
            protas_image[2].SetActive(true);
        }
    }
}
