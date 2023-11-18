using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    int vel = 30;
    public GameObject brazo;
    private Vector3 direccion;
    private float velocidad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoverBala();
        //if (gameObject.GetComponent<SpriteRenderer>().flipX == true && !brazo.GetComponent<Brazo_controller>().Get_Brazo_Arriba() && !brazo.GetComponent<Brazo_controller>().Get_Brazo_Abajo()) transform.Translate(Vector2.left * vel * Time.deltaTime);
        //else if (gameObject.GetComponent<SpriteRenderer>().flipX == false && !brazo.GetComponent<Brazo_controller>().Get_Brazo_Arriba() && !brazo.GetComponent<Brazo_controller>().Get_Brazo_Abajo()) transform.Translate(Vector2.right * vel * Time.deltaTime);
        //else if (brazo.GetComponent<Brazo_controller>().Get_Brazo_Arriba()) transform.Translate(Vector2.down * vel * Time.deltaTime);
        //else if (brazo.GetComponent<Brazo_controller>().Get_Brazo_Abajo()) transform.Translate(Vector2.up * vel * Time.deltaTime);
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("wall"))
            Destroy(gameObject);

    }
    public void InicializarBala(Vector3 direccion, float velocidad)
    {
        this.direccion = direccion;
        this.velocidad = velocidad;
    }

    void MoverBala()
    {
        transform.Translate(direccion * velocidad * Time.deltaTime);

        // Puedes agregar lógica para destruir la bala cuando está fuera de la pantalla u otras condiciones.
    }
}
