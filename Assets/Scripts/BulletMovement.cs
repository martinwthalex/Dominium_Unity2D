using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    int vel = 30;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<SpriteRenderer>().flipX == true) transform.Translate(Vector2.left * vel * Time.deltaTime);
        else transform.Translate(Vector2.right * vel * Time.deltaTime);
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("wall"))
            Destroy(gameObject);

    }
}
