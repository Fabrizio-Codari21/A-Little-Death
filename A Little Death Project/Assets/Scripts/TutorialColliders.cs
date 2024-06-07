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

    private void Start()
    {
        if (tutorialValue == 1)
        {
            textToPrint = "Bien, comenzemos con la caceria, usa (WAD) para moverte y saltar";
        }
        else if (tutorialValue == 2)
        {
            textToPrint = "En el proximo páramo encontraras a tu primer enemigo, utiliza tu guadaña (ESPACIO) para eliminarlo";
        }
        else if (tutorialValue == 3)
        {
            textToPrint = "Utiliza (SHIFT) para usar la habilidad del venado y llegar al otro lado";
        }
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
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
        yield return new WaitForSeconds(10f);
        TMPTutorial.text = "";
        tutorialBox.SetActive(false);
    }
}
