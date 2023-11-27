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
        Set_panel_arma(false,"");
        Set_protas_image(PlayerController.vidas);
    }

    private void Set_panel_arma(bool panel_setActive,string _texto)
    {
        panel_arma.SetActive(panel_setActive);
        texto_arma.text = _texto;
    }

    private void Set_protas_image(int _vidas)
    {
        _vidas -= 1;
        protas_image[_vidas].SetActive(false);
    }
}
