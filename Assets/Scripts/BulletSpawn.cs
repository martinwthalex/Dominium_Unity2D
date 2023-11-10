using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    public GameObject bullet;
    //public GameObject brazo;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            Quaternion quaternion = new Quaternion(0, 0, 0, 0);
            if (gameObject.GetComponentInParent<SpriteRenderer>().flipX == true)
            {
                bullet.GetComponent<SpriteRenderer>().flipX = true;
                
            }
            else
            {
                bullet.GetComponent<SpriteRenderer>().flipX = false;
                
            }
            Instantiate(bullet, gameObject.transform.position, quaternion);
        }

    }
}
   