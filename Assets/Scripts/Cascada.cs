using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cascada : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bala") && GameObject.FindGameObjectWithTag("enemy") == null)
        {
            
            Wave_spawner.playing_waves = true;
            Destroy(gameObject);
        }
    }
}
