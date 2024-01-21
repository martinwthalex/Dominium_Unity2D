using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_controller : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetInteger("Vida", PlayerController.vidas);
        if (Input.GetKeyDown(KeyCode.Q)) PlayerController.RestarVidas();
    }
}
