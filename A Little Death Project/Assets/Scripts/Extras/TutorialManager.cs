using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    private void Awake()
    {
        instance = this;
    }
    
    List<string> tutorialBoxes = new List<string>()
    {
        "Utiliza tu guadaña con <b>Click Izquierdo</b> para derrotar a las almas.",

        "Con <b>'Click Derecho'</b> vas a poder usar la habilidad secundaria brindada por tu enemigo para desplazarte.",

        "Si necesitas liberar tu alma, utiliza la <b>E</b>.",

        "Con <b>'Click Derecho'</b> vas a poder usar la habilidad secundaria brindada por este enemigo para llegar mas alto.",
    };

    int tutorialCount = 0;
    public TutorialBox firstBox = default;
    public TutorialBox secondBox = default;

    [HideInInspector] public bool tutorialIsActive;

    public void ChangeTutorial(int index = -1)
    {
        print("cambiamos tutorial");

        var currentBox = firstBox.gameObject.activeInHierarchy ? firstBox : secondBox;
        var newBox = firstBox.gameObject.activeInHierarchy ? secondBox : firstBox;

        currentBox.movingBack = true;
        newBox.gameObject.SetActive(true);
        newBox.moving = true;
        newBox.textToPrint = tutorialBoxes[index != -1 ? index : tutorialCount];
        tutorialIsActive = true;
        tutorialCount = index != -1 ? index + 1 : tutorialCount + 1;         
    }

    public void SkipTutorial()
    {
        var currentBox = firstBox.gameObject.activeInHierarchy ? firstBox : secondBox;

        currentBox.movingBack = true;
        tutorialIsActive = false;
    }
}
