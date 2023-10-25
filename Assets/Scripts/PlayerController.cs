using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public int vel = 5, jumpForce = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement(vel);
    }

    void Movement(int vel)
    {
        rb.velocity = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.RightArrow))
            rb.velocity += new Vector2(vel, 0);
        if (Input.GetKey(KeyCode.LeftArrow))
            rb.velocity += new Vector2(-vel, 0);
        if (Input.GetKeyDown(KeyCode.Space) && OnFloor())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

    }
    
    private bool OnFloor()
    {
        return Mathf.Approximately(rb.velocity.magnitude, 0);
        //Vector3 origen = transform.position;
        //Vector3 direccion = Vector3.down;
        //float distancia = 0.2f;

        //RaycastHit2D hit = Physics2D.Raycast(origen, direccion, distancia);
        //Debug.DrawRay(origen, direccion * distancia, Color.red);

        //onFloor = false;
        //if (hit) // Asegurarme de que el rayo ha golpeado en algo.
        //{
        //    if (hit.collider.tag == "Floor")
        //        onFloor = true;
        //}
    }
}
