using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        //rb_bullet = bullet.GetComponent<Rigidbody2D>();
        //direction = new Vector2(rb_bullet.velocity.x, rb_bullet.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Quaternion quaternion = new Quaternion(0,0,0,0);
            Instantiate(bullet,transform.position,quaternion);
        }
        
    }
    
}
