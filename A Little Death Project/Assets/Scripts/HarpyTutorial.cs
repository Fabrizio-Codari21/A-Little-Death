using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HarpyTutorial : MonoBehaviour
{
    private bool paused;
    public WingsHealth health;
    private bool alreadyPaused;
    public GameObject tutorialBox;
    public TextMeshProUGUI TMPTutorial;

    private void Update()
    {
        if (health.currentHealth <= 0 && alreadyPaused == false)
        {
            TimePause();
            tutorialBox.SetActive(true);
            TMPTutorial.text = "Va a haber una gran cantidad de oponentes en tu camino, algunos incluso van a tener mas de una habilidad para elegir, como una de ataque, que podés elegir con la <b>flecha derecha</b>.";
        }

        if (paused)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow))
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
