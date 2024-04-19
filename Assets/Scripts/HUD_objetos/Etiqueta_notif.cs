using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Etiqueta_notif : MonoBehaviour
{
    #region Variables
    public Image etiqueta_notif;
    public List<TMP_Text> titulo_etiqueta, texto_etiqueta;
    #endregion

    private void Start()
    {
        etiqueta_notif.gameObject.SetActive(false);
    }
}
