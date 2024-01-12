using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave_spawner : MonoBehaviour
{
    public enum Spawn_state { SPAWNING, WAITING, COUNTING };
    public static bool playing_waves;
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }
    public Wave[] waves;

    public Transform[] spawn_points;

    private int next_wave = 0;
    public float time_between_waves = 5f;
    private float wave_countDown;
    private float search_countDown = 1f;
    private Spawn_state state = Spawn_state.COUNTING;

    private void Start()
    {
        wave_countDown = time_between_waves;
        playing_waves = false;// cuando eliminas a todos los enemigos, esto se pone a true

        if(spawn_points.Length == 0)
        {
            Debug.LogError("No spawn points referenced");
        }
    }

    private void Update()
    {
        if (playing_waves)
        {
            if (state == Spawn_state.WAITING)
            {
                if (!EnemyIsAlive())
                {
                    WaveCompleted();
                }
                else
                {
                    return;
                }
            }

            if (wave_countDown <= 0)
            {
                if (state != Spawn_state.SPAWNING)
                {
                    // Start spawning wave 

                    StartCoroutine(SpawnWave(waves[next_wave]));
                }
            }
            else
            {
                wave_countDown -= Time.deltaTime;
            }
        }
    }

    void WaveCompleted()
    {
        print("Wave completed!");

        state = Spawn_state.COUNTING;
        wave_countDown = time_between_waves;
        if(next_wave + 1 > waves.Length - 1)
        {
            next_wave = 0;
            print("ALL WAVES COMPLETE!");
            Brazo_controller.Set_Can_Disparo_Plataformas(true);
        }
        else
        {
            next_wave++;
        }
    }

    bool EnemyIsAlive()
    {
        search_countDown -= Time.deltaTime;
        if(search_countDown <= 0f)
        {
            search_countDown = 1f;
            if (GameObject.FindGameObjectWithTag("enemy") == null)// cambiar el tag dependiendo si son del estomago o de los pulmones
            {
                return false;                                       // otra opcion es set active false a los enemigos que no esten en la sala actual
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        print("Spawning wave: " + _wave.name);
        
        state = Spawn_state.SPAWNING;

        for(int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = Spawn_state.WAITING;
        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        print("Spawn enemy: " + _enemy.name);
        Transform _sp = spawn_points[Random.Range(0,spawn_points.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }
}
