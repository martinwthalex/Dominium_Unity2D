using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basico_enem : MonoBehaviour
{
    static bool limite;
    Rigidbody2D rb;
    private void Start()
    {
        limite = false;
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        
    }
    public static void Set_limite(bool limite_)
    {
        limite = limite_;
    } 
}
