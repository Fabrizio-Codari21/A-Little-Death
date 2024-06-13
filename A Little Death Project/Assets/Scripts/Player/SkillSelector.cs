using System.Collections;
using System.Collections.Generic;
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

    void Update()
    {
        Selection();
    }

    public void Selection()
    {
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
                        selecting = false;
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
                        selecting = false;
                        Deactivate();
                    }
                    else
                    {
                        wingsDash.DJump(false);
                        dashManager.dashID = enemyID;
                        Debug.Log(enemyID);
                        selecting = false;
                        Deactivate();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (attackManager.attackType.ContainsKey(enemyID))
                    {
                        habilityUI.AlasAtaque();
                        attackManager.primaryFire = enemyID;
                        selecting = false;
                        Deactivate();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    Debug.Log("AAAAAAAAA");
                    selecting = false;
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
                    selecting = false;
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
                        selecting = false;
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
                        selecting = false;
                        Deactivate();
                    }
                    else
                    {
                        habilityUI.FuerzaDash();
                        wingsDash.DJump(false);
                        dashManager.dashID = enemyID;
                        Debug.Log(enemyID);
                        selecting = false;
                        Deactivate();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (attackManager.attackType.ContainsKey(enemyID))
                    {
                        attackManager.primaryFire = enemyID;
                        selecting = false;
                        Deactivate();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    Debug.Log("AAAAAAAAA");
                    selecting = false;
                    Deactivate();
                }
            }
        }
    }

    public void Deactivate()
    {
        player.transform.GetChild(1).gameObject.SetActive(false);
        player.transform.GetChild(0).gameObject.SetActive(false);
    }
}