using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    public GameObject bullet,jugador;
    private Quaternion original_bullet_rotation;
    //public GameObject brazo;
    // Start is called before the first frame update
    void Start()
    {
        original_bullet_rotation = new Quaternion();
    }
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            
            if (jugador.GetComponent<PlayerController>().GetFlipX() && !gameObject.GetComponent<Brazo_controller>().Get_Brazo_Arriba() && !gameObject.GetComponent<Brazo_controller>().Get_Brazo_Abajo())
            {
                bullet.GetComponent<SpriteRenderer>().flipX = true;
                print("IZQUIERDA");
            }
            else if(gameObject.GetComponent<Brazo_controller>().Get_Brazo_Arriba())
            {
                bullet.GetComponent<SpriteRenderer>().flipX = false;
                if(bullet.transform.rotation.z == 0)
                {
                    bullet.transform.Rotate(Vector3.forward * 90f);
                }
                
                print("ARRIBA");
            }
            else if (gameObject.GetComponent<Brazo_controller>().Get_Brazo_Abajo())
            {
                bullet.GetComponent<SpriteRenderer>().flipX = false;
                if (bullet.transform.rotation.z == 0)
                    bullet.transform.Rotate(Vector3.forward * 90f);
                print("ABAJO");
            }
            else
            {
                bullet.GetComponent<SpriteRenderer>().flipX = false;
                transform.rotation = original_bullet_rotation;
                print("DERECHA");
            }
            Instantiate(bullet, gameObject.transform.position, bullet.transform.rotation);
        }

    }
}
   