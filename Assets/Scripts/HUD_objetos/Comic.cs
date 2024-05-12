using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Comic : MonoBehaviour
{
    public GameObject right_arrow;
    public Image button_right_arrow;
    public float timer;
    private void Start()
    {
        Time.timeScale = 1;
        right_arrow.SetActive(false);
        button_right_arrow.enabled = false;
        timer = 5;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            right_arrow.SetActive(true);
            button_right_arrow.enabled = true;
        }
    }

    public void LoadPulmones()
    {
        timer = 5;
        SceneManager.LoadScene("SampleScene");
    }
}
