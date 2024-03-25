using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plat_rompe : MonoBehaviour
{
	#region Variables
	const float tiempo_max = 2; // tiempo maximo de vida de la plataforma
	public float timer_stay, timer_exit; // 1er temporizador --> se activa cuando el jugador pisa la plataforma
                                         // 2do temporizador --> se activa cuando el jugador salta de la plataforma
    const float tiempo_antes_de_regenerar = 1.5f;// el tiempo que tiene que pasar el jugador sin pisar la plataforma para
                                                 // que esta se regenere (reinicio de los temporizadores anteriores)
    bool enter_plataforma = false;
    bool exit_plataforma = false;

    public float vel_agitacion = 60f; //how fast it shakes
    public float cantidad_agitacion = 0.02f; //how much it shakes
    Vector2 pos_inicial;
    #endregion

    #region Entradas y salidas del jugador de las plataformas
    private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
            enter_plataforma = true;
            exit_plataforma = false;
        }
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			exit_plataforma = true;
            enter_plataforma = false;
		}
	}

    #endregion

    
    #region Inicializar temporizadores
    private void Start()
	{
        timer_stay = tiempo_max;
        timer_exit = tiempo_antes_de_regenerar;

        pos_inicial.x = transform.position.x;
        pos_inicial.y = transform.position.y;
    }
    #endregion

	private void Update()
	{
        #region Jugador entra en la plataforma, se activa temporizador
        if (enter_plataforma)
        {
            timer_stay -= Time.deltaTime;
            ShakePlataforma(true);
            if(timer_stay <= 0)
            {
                PlataformaViva(false);
            }
        }
        #endregion

        //CONTINUAR AQUI 
        
        #region Jugador sale de la plataforma, se activa el temporizador para regenerarla
        if (exit_plataforma)
        {
            timer_exit -= Time.deltaTime;
            if (timer_exit <= 0)
            {
                ShakePlataforma(false);
                enter_plataforma = false;
                exit_plataforma = false;
                timer_stay = tiempo_max;
                timer_exit = tiempo_antes_de_regenerar;
            }
        }
        #endregion
    }

    #region Activar y desactivar plataforma
    void PlataformaViva(bool viva)
    {
        gameObject.SetActive(viva);
    }
    #endregion

    #region Shake Plataforma
    void ShakePlataforma(bool shake)
    {
        if (shake)
        {
            gameObject.transform.position = new Vector3((pos_inicial.x + Mathf.Sin(Time.time * vel_agitacion) * cantidad_agitacion),
            pos_inicial.y + (Mathf.Sin(Time.time * vel_agitacion) * cantidad_agitacion), 0);
        }
    }
    #endregion
}
