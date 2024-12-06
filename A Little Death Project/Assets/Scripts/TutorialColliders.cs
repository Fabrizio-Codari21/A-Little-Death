using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialColliders : MonoBehaviour
{
    //public TutorialBox tutorialBox;
    //public TutorialBox tutorialBoxVenado;
    public GameObject tutorialBoxObj;
    public string wantToPrint;
    public float time;
    public int printIndex;
    [SerializeField] bool venado;
    [SerializeField] bool activated = false;

    private void Update()
    {
        if (this.Inputs(MyInputs.Skip) && TutorialManager.instance.tutorialIsActive)
        {
            TutorialManager.instance.SkipTutorial();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && activated == false)
        {
            activated = true;
            Debug.Log("Calling Tutorial");
            TutorialManager.instance.ChangeTutorial(printIndex != default ? printIndex : -1);
        }
    }

    /*public void ActivateTutorial(bool active)
    {
        if (active)
        {
            activated = true;
            tutorialBoxObj.SetActive(true);
            tutorialBox.textToPrint = wantToPrint;
            if(!venado && tutorialBoxVenado.isActiveAndEnabled)
            {
                tutorialBoxVenado.movingBack = true;
            }
        } 
        else
        {
            tutorialBox.movingBack = true;
            Destroy(gameObject, 0.01f);
        }
    }*/

/*BACKLOG DEL TEXTO POR LAS DUDAS DE QUE SE BORRE
Utiliza tu guadaña con <b>Click Izquierdo</b> para derrotar a las almas.
Con <b>'Click Derecho'</b> vas a poder usar la habilidad secundaria brindada por tu enemigo para desplazarte.
Si necesitas liberar tu alma, utiliza la <b>E</b>.
Con <b>'Click Derecho'</b> vas a poder usar la habilidad secundaria brindada por este enemigo para llegar mas alto.
*/

}
