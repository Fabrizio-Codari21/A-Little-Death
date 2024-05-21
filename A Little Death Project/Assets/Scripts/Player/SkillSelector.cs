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
            //este if es temporal despues lo tenes que sacar pelotudo!
            if (enemyID == "alas")
            {
                //player.transform.GetChild(0).gameObject.SetActive(true);
                player.transform.GetChild(1).gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (attackManager.attackType.ContainsKey(enemyID))
                    {
                        attackManager.secondaryFire = enemyID;
                        selecting = false;
                        player.transform.GetChild(1).gameObject.SetActive(false);
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
                        player.transform.GetChild(1).gameObject.SetActive(false);
                    }
                    else
                    {
                        wingsDash.DJump(false);
                        dashManager.dashID = enemyID;
                        Debug.Log(enemyID);
                        selecting = false;
                        player.transform.GetChild(1).gameObject.SetActive(false);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (attackManager.attackType.ContainsKey(enemyID))
                    {
                        habilityUI.AlasAtaque();
                        attackManager.primaryFire = enemyID;
                        selecting = false;
                        player.transform.GetChild(1).gameObject.SetActive(false);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    Debug.Log("AAAAAAAAA");
                    selecting = false;
                    player.transform.GetChild(1).gameObject.SetActive(false);
                }
            }
            else
            {
                player.transform.GetChild(0).gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (attackManager.attackType.ContainsKey(enemyID))
                    {
                        attackManager.secondaryFire = enemyID;
                        selecting = false;
                        player.transform.GetChild(0).gameObject.SetActive(false);
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
                        player.transform.GetChild(0).gameObject.SetActive(false);
                    }
                    else
                    {
                        habilityUI.FuerzaDash();
                        wingsDash.DJump(false);
                        dashManager.dashID = enemyID;
                        Debug.Log(enemyID);
                        selecting = false;
                        player.transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (attackManager.attackType.ContainsKey(enemyID))
                    {
                        attackManager.primaryFire = enemyID;
                        selecting = false;
                        player.transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    Debug.Log("AAAAAAAAA");
                    selecting = false;
                    player.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }
    }
}