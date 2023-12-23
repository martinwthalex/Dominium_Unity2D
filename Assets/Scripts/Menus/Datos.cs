using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Datos
{
    [SerializeField]public static int escena { get; set; }//para saber a traves de que escena ha llegado el usuario al menu de opciones
                                          //(desde MENU INICIAL "0", o desde el JUEGO menu pausa "1")
}