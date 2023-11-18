using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    public GameObject bullet,jugador;
    public Transform cañon;
    private Quaternion original_bullet_rotation;
    //public GameObject brazo;
    // Start is called before the first frame update
    void Start()
    {
        //original_bullet_rotation = new Quaternion();
        //bullet.transform.rotation = original_bullet_rotation;
    }
    // Update is called once per frame
    void Update()
    {
        
        //if (Input.GetKeyDown(KeyCode.X))
        //{
            
        //    if (jugador.GetComponent<PlayerController>().GetFlipX() && !gameObject.GetComponent<Brazo_controller>().Get_Brazo_Arriba() && !gameObject.GetComponent<Brazo_controller>().Get_Brazo_Abajo())
        //    {
        //        bullet.GetComponent<SpriteRenderer>().flipX = true;
        //        bullet.transform.rotation = original_bullet_rotation;
                
        //    }
        //    if(gameObject.GetComponent<Brazo_controller>().Get_Brazo_Arriba())
        //    {
        //        bullet.GetComponent<SpriteRenderer>().flipX = false;
                
        //        bullet.transform.rotation = original_bullet_rotation;
        //        bullet.transform.Rotate(Vector3.forward * 90f);
                
        //    }
        //    if (gameObject.GetComponent<Brazo_controller>().Get_Brazo_Abajo())
        //    {
        //        bullet.GetComponent<SpriteRenderer>().flipX = false;

        //        bullet.transform.rotation = original_bullet_rotation;
        //        bullet.transform.Rotate(Vector3.forward * 270f);

        //    }
        //    if(!jugador.GetComponent<PlayerController>().GetFlipX() && !gameObject.GetComponent<Brazo_controller>().Get_Brazo_Arriba() && !gameObject.GetComponent<Brazo_controller>().Get_Brazo_Abajo())
        //    {
        //        bullet.GetComponent<SpriteRenderer>().flipX = false;
        //        bullet.transform.rotation = original_bullet_rotation;
        //        //transform.rotation = original_bullet_rotation;
                
        //    }
        //    Instantiate(bullet, cañon.position, bullet.transform.rotation);
        //}

    }
}
   