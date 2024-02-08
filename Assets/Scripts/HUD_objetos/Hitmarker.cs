using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hitmarker : MonoBehaviour
{
    Transform enemigo;
    private void Update()
    {
        if (this != null) this.transform.position = enemigo.position;
    }
    public void Inicializar_enemigo_pos(Transform enemigo_)
    {
        if (enemigo_ != null) enemigo = enemigo_;
    }
}
