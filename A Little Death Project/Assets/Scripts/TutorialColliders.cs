using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialColliders : MonoBehaviour
{
    public TutorialBox tutorialBox;
    public GameObject tutorialBoxObj;
    public string wantToPrint;
    public float time;

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
            tutorialBoxObj.SetActive(true);
            tutorialBox.textToPrint = wantToPrint;
        }
        else
        {
            tutorialBox.movingBack = true;
        }
    }

    //BACKLOG DEL TEXTO POR LAS DUDAS DE QUE SE BORRE
    //Adelante vas a ver un alma perdida. Puedes usar tu habilidad principal, la guadaña, con <b>Click Izquierdo</b>, para segarla.
    //Con <b>'Click Derecho'</b> vas a poder usar la habilidad secundaria brindada por tu enemigo para desplazarte.

}
