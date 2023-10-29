using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Quaternion quaternion = new Quaternion(0,0,0,0);
            if(gameObject.GetComponent<SpriteRenderer>().flipX == true)
            {
                bullet.GetComponent<SpriteRenderer>().flipX = true;
            }
            else bullet.GetComponent<SpriteRenderer>().flipX = false;
            Instantiate(bullet,transform.position,quaternion);
        }
        
    }
    
}
