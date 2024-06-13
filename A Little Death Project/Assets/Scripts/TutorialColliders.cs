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
            textToPrint = "Hola hija mía, recordá, paso por paso, presioná <b>'A'</b> y <b>'D'</b> para moverte y saltar es tan fácil como apretar <b>'W'</b>.";
        }
        else if (tutorialValue == 2)
        {
            textToPrint = "Adelante vas a ver un alma perdida. Tu tarea es recuperarlas para que regresen al reino de tu Padre usando la guadaña, solo tenés que apretar <b>'ESPACIO'</b>.";
        }
        else if (tutorialValue == 3)
        {
            textToPrint = "Con <b>'SHIFT'</b> vas a poder usar el poder brindado por el gran venado para escapar de peligros y saltar grandes distancias.";
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
