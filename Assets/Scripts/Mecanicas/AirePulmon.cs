using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirePulmon : MonoBehaviour
{
    #region Variables
    public float multiplicador = 0.5f;
    PlayerController playerController;
    #endregion

    #region Detectar PlayerController
    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    #endregion

    #region TriggerEnter y TriggerExit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
        playerController.TunelDeViento(true, multiplicador);
        playerController.SetVelFramesMov(true, multiplicador);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController.TunelDeViento(false, multiplicador);
            playerController.SetVelFramesMov(false, multiplicador);
        }
    }
    #endregion
}
