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
            textToPrint = "Hola hija mía, recordá, paso por paso, " +
                "presioná 'A' y 'D' para moverte y saltar es tan fácil como apretar 'W'.";
        }
        else if (tutorialValue == 2)
        {
            textToPrint = "Adelante vas a ver un alma perdida. " +
                "Tu tarea es recuperarlas para que regresen al reino de tu Padre usando la guadaña, solo tenés que apretar 'ESPACIO'.";
        }
        else if (tutorialValue == 3)
        {
            textToPrint = "Con 'SHIFT' vas a poder usar el poder brindado por el gran venado para escapar de peligros " +
                "y saltar grandes distancias.";
        }
        else if (tutorialValue == 4)
        {
            textToPrint = "Va a haber una gran cantidad de oponentes en tu camino, algunos incluso van a tener mas de una habilidad para elegir, " +
                "como una de ataque, que podés elegir con la flecha derecha.";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            tutorialBox.SetActive(true);
            TMPTutorial.text = textToPrint;
            StartCoroutine(DialogueCooldown());
        }
    }

    private IEnumerator DialogueCooldown()
    {
        yield return new WaitForSecondsRealtime(time);
        TMPTutorial.text = "";
        tutorialBox.SetActive(false);
    }
}
