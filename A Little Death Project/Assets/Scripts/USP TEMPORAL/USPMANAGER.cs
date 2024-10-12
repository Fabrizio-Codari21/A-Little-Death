using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class USPMANAGER : MonoBehaviour
{
    private bool paused;
    public USPTutorial health;
    private bool alreadyPaused;
    public GameObject tutorialBox;
    public TextMeshProUGUI TMPTutorial;

    public HabilityUI habilityUI;
    public DashManager dashManager;
    public AttackManager attackManager;
    public ThaniaSkills defaultSkills;
    public CharacterSkillSet defaultSkillSet;
    public PlayerSkillManager skillManager;

    public GameObject thaniaSprite;
    public GameObject deerSprite;

    public TimerBar timerBar;
    public GameObject UI;

    private void Update()
    {
        if (health.currentHealth <= 0 && this.alreadyPaused == false)
        {
            TimePause();
            tutorialBox.SetActive(true);
            TMPTutorial.text = "Gracias a tu arma, con la <b>q</b>, podras poseer a tu enemigo.";
        }

        if (paused)
        {
            if (this.Inputs(MyInputs.Possess))
            {
                habilityUI.FuerzaDash();
                dashManager.dashID = "fuerza";
                attackManager.primaryFire = "deer";
                Debug.Log("fuerza");
                thaniaSprite.SetActive(false);
                deerSprite.SetActive(true);
                StartCoroutine(timer());
                UI.gameObject.SetActive(true);
                timerBar.timerActive = true;
                Time.timeScale = 1;
                paused = false;
                this.alreadyPaused = true;
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

    IEnumerator timer()
    {
        yield return new WaitForSeconds(10);

        habilityUI.Default();
        dashManager.dashID = "base";
        attackManager.primaryFire = "base";
        Debug.Log("base");

        thaniaSprite.SetActive(true);
        deerSprite.SetActive(false);

        defaultSkills.DefineSkills(defaultSkillSet);
        skillManager.BuildSkillSet(defaultSkillSet.primarySkill, defaultSkillSet.secondarySkill);
    }
}
