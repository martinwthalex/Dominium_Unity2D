using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cascada : MonoBehaviour
{
    public Transform player;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bala") && Wave_spawner.playing_waves == false)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && player.position.x > this.transform.position.x)
        {
            Wave_spawner.playing_waves = true;
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255,255,255,255);
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

}
