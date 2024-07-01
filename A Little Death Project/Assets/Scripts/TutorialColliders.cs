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

    private void Start()
    {
        if (tutorialValue == 1)
        {
            textToPrint = "Bien Thania, presiona <b>'A'</b> y <b>'D'</b> para moverte y  <b>'W'</b> para saltar.";
        }
        else if (tutorialValue == 2)
        {
            textToPrint = "Adelante vas a ver un alma perdida. Tu tarea es cazarlas usando la guadaña, solo tenes que apretar <b>'ESPACIO'</b>.";
        }
        else if (tutorialValue == 3)
        {
            textToPrint = "Con <b>'SHIFT'</b> vas a poder usar el poder brindado por tu enemigo para saltar grandes distancias.";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            tutorialBox.SetActive(true);
            TMPTutorial.text = textToPrint;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            tutorialBox.SetActive(false);
            TMPTutorial.text = "";
        }
    }
}
