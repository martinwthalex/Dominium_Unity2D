using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubble_enem_pulmon : MonoBehaviour
{
    Transform enemigo;
    public static bool player_inBubble = false;
    private void Update()
    {
        if (this != null) this.transform.position = enemigo.position;
        
    }
    public void Inicializar_bubble_pos(Transform enemigo_)
    {
        if (enemigo_ != null) enemigo = enemigo_;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.Set_player_atributes(5,5);
            player_inBubble = true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.Set_player_atributes(20, 20);
            player_inBubble = false;
        }
    }
    public static bool Get_player_inBubble()
    {
        return player_inBubble;
    }
}
