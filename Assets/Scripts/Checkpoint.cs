using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    #region Variables
    public Transform respawnPoint;
    SpriteRenderer sr;
    public Sprite activado = null, desactivado = null;
    BoxCollider2D coll;

    static bool reiniciar_checkpoints = false;
    #endregion

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        sr.sprite = desactivado;
    }

    private void Update()
    {
        if (reiniciar_checkpoints)
        {
            sr.sprite = desactivado;
            coll.enabled = true;
            reiniciar_checkpoints = false;
        }
        //else
        //{
        //        sr.sprite = desactivado;
        //    //if (coll.enabled)
        //    //{
        //    //}
        //}
    }

    #region Trigger checkpoint
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && sr.sprite == desactivado)
        {
            PlayerController.Instance.UpdateCheckPoint(respawnPoint.position);
            sr.sprite = activado;
            coll.enabled = false;
        }
    }
    #endregion

    #region Reiniciar Checkpoints
    public static void ReiniciarCheckpoints()
    {
        reiniciar_checkpoints = true;
    }
    #endregion
}
