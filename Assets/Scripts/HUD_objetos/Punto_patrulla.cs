using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class Punto_patrulla : MonoBehaviour
{
    Transform GameManager;
    private void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameController").transform;
        this.transform.SetParent(GameManager);
    }
}
