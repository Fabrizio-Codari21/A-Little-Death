using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;

public class DeerTutorial : MonoBehaviour
{
    private bool paused;
    public VenadoTutorial health;
    private bool alreadyPaused;
    public GameObject tutorialBox;
    public TextMeshProUGUI TMPTutorial;

    private void Update()
    {
        if(health.currentHealth <= 0 && alreadyPaused == false)
        {
            TimePause();
            tutorialBox.SetActive(true);
            TMPTutorial.text = "Presiona (UP ARROW) para absorver la habilidad de movimiento del venado, recuerda que cada enemigo tendra habilidades distintas";
        }

        if (paused)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Time.timeScale = 1;
                paused = false;
                alreadyPaused = true;
                TMPTutorial.text = "";
                tutorialBox.SetActive(false);
            }
        }
    }

    void TimePause()
    {
        Time.timeScale = 0;
        paused = true;
    }
}