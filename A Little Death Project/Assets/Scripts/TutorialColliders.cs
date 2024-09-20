using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialColliders : MonoBehaviour
{
    public GameObject tutorialBox;
    public TextMeshProUGUI TMPTutorial;
    string textToPrint;
    public int tutorialValue;
    public float time;

    private void Update()
    {
        if (tutorialValue == 1)
        {
            textToPrint = "Bien Thania, presiona <b>'A'</b> y <b>'D'</b> para moverte y  <b>'W'</b> para saltar.";
        }
        else if (tutorialValue == 2)
        {
            textToPrint = "Adelante vas a ver un alma perdida. Puedes usar tu habilidad principal, la guadaña, con <b>Click Izquierdo</b>, para segarla.";
        }
        else if (tutorialValue == 3)
        {
            textToPrint = "Con <b>'Click Derecho'</b> vas a poder usar la habilidad secundaria brindada por tu enemigo para desplazarte.";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            ActivateTutorial(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            ActivateTutorial(false);
        }
    }

    public void ActivateTutorial(bool active)
    {
        if (active)
        {
            tutorialBox.SetActive(true);
            TMPTutorial.text = textToPrint;
            
        }
        else
        {
            tutorialBox.SetActive(false);
            TMPTutorial.text = "";
            tutorialValue++;
        }
    }
}
