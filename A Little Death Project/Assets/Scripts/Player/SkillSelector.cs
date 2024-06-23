using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillSelector : MonoBehaviour
{
    public bool selecting = false;
    public AttackManager attackManager;
    public DashManager dashManager;
    public string enemyID;
    public GameObject player;
    public WingsDash wingsDash;
    public HabilityUI habilityUI;
    public bool cooldownStarted = false;
    public bool cooldownEnded = false;

    [SerializeField] float timerCooldown;
    public float timeCooldown;

    void Update()
    {
        Selection();
    }

    public void Selection()
    {
        if (cooldownEnded == true)
        {
            cooldownStarted = false;
            Deactivate();
            cooldownEnded = false;
        }

        if (cooldownStarted == false && selecting)
        {
            cooldown();
        }

        if (selecting == true)
        {
            if (enemyID == "alas")            //este if es temporal despues lo tenes que sacar pelotudo!
            {
                player.transform.GetChild(1).gameObject.SetActive(true);
                player.transform.GetChild(0).gameObject.SetActive(false);

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (attackManager.attackType.ContainsKey(enemyID))
                    {
                        attackManager.secondaryFire = enemyID;
                        Deactivate();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (enemyID == "alas")
                    {
                        habilityUI.AlasDash();
                        wingsDash.DJump(true);
                        dashManager.dashID = enemyID;
                        Debug.Log(enemyID);
                        Deactivate();
                    }
                    else
                    {
                        wingsDash.DJump(false);
                        dashManager.dashID = enemyID;
                        Debug.Log(enemyID);
                        Deactivate();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (attackManager.attackType.ContainsKey(enemyID))
                    {
                        habilityUI.AlasAtaque();
                        attackManager.primaryFire = enemyID;
                        Deactivate();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    Debug.Log("AAAAAAAAA");
                    Deactivate();
                }
            }
            else if (enemyID == "tutorial")
            {
                player.transform.GetChild(0).gameObject.SetActive(true);
                player.transform.GetChild(1).gameObject.SetActive(false);
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    habilityUI.FuerzaDash();
                    wingsDash.DJump(false);
                    dashManager.dashID = "fuerza";
                    Debug.Log("fuerza");
                    Deactivate();
                }
            }
            else
            {
                player.transform.GetChild(0).gameObject.SetActive(true);
                player.transform.GetChild(1).gameObject.SetActive(false);

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (attackManager.attackType.ContainsKey(enemyID))
                    {
                        attackManager.secondaryFire = enemyID;
                        Deactivate();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (enemyID == "alas")
                    {
                        wingsDash.DJump(true);
                        dashManager.dashID = enemyID;
                        Debug.Log(enemyID);
                        Deactivate();
                    }
                    else
                    {
                        habilityUI.FuerzaDash();
                        wingsDash.DJump(false);
                        dashManager.dashID = enemyID;
                        Debug.Log(enemyID);
                        Deactivate();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (attackManager.attackType.ContainsKey(enemyID))
                    {
                        attackManager.primaryFire = enemyID;
                        Deactivate();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    Debug.Log("AAAAAAAAA");
                    Deactivate();
                }
            }
        }
    }

    void cooldown()
    {
        if (timerCooldown >= timeCooldown)
        {
            timeCooldown += Time.deltaTime;
        }
        else
        {
            cooldownStarted = true;
            cooldownEnded = true;
            timeCooldown = 0f;
        }
    }

    public void Deactivate()
    {
        player.transform.GetChild(1).gameObject.SetActive(false);
        player.transform.GetChild(0).gameObject.SetActive(false);
        timeCooldown = 0f; 
        selecting = false;
    }
}