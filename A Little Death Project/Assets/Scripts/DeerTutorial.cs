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
        if (health.currentHealth <= 0 && alreadyPaused == false)
        {
            TimePause();
            tutorialBox.SetActive(true);
            TMPTutorial.text = "Como agradecimiento por liberar su alma, este ciervo te otorga una de sus habilidades, con la <b>flecha de arriba</b> podés obtener la habilidad de movimiento. " +
                "Recordá que diversos enemigos darán diversas habilidades, y podés combinar y mezclarlas como quieras!";
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