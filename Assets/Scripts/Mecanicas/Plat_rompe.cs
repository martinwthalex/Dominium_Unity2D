using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plat_rompe : MonoBehaviour
{
	#region Variables
	const float tiempo_max = 3;
	public float timer_stay, timer_exit;
	const float tiempo_antes_de_regenerar = tiempo_max;
	bool[] fases = { false, false, false};
	#endregion

	#region Mecanica de inicio de temporizador
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
            if (fases[0] != true)
            {
                timer_stay = tiempo_max;
                fases[0] = true;
            }
        }
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			fases[2] = true;
		}
	}

	private void Start()
	{
		timer_exit = tiempo_antes_de_regenerar;
	}

	private void Update()
	{
		if (fases[0] == true)
		{
            timer_stay -= Time.deltaTime;
            if (timer_stay < 0 && fases[1] != true)
            {
                fases[1] = true;
				PlataformaViva(false);
				fases[0] = false;
                fases[1] = false;
            }
        }

        if (fases[2] == true)
		{
            timer_exit -= Time.deltaTime;
			if(timer_exit < 0)
			{

			}
        }
    }

	void PlataformaViva(bool viva)
	{
		gameObject.SetActive(viva);
	}
	#endregion
}
