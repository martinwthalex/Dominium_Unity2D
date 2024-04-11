using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baba_desliza : MonoBehaviour
{
    #region Variables
    float multiplicador = 1.3f;
    float mult_frames_anim = 2f;
    #endregion

    #region TriggerEnter y TriggerExit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.BabaDeslizante(multiplicador);
            PlayerController.Instance.SetVelFramesMov(true, mult_frames_anim);
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.BabaDeslizante();
            PlayerController.Instance.Set_player_atributes();
        }       
    }
    #endregion
}
