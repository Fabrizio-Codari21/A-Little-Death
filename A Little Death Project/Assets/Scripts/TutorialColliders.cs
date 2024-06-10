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
            textToPrint = "Hiya sweetie! Remember, one step at a time, " +
                "you can use 'A' and 'D' to move. And jumping is as easy as pressing 'W'";
        }
        else if (tutorialValue == 2)
        {
            textToPrint = "You'll soon encounter a lost soul. " +
                "It is your task to retrieve it back to your Father's realm with your scythe by clicking 'SPACE'";
        }
        else if (tutorialValue == 3)
        {
            textToPrint = "With 'SHIFT' you may utilize the greatness bestowed upon you by the stag" +
                " to shift away from danger and clear large gaps";
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
