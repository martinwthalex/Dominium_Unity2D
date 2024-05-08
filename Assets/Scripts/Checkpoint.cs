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
    #endregion

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        sr.sprite = desactivado;
    }

    #region Trigger checkpoint
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController.Instance.UpdateCheckPoint(respawnPoint.position);
            sr.sprite = activado;
            coll.enabled = false;
        }
    }
    #endregion
}
