using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorgonSkills : MonoBehaviour, ISkillDefiner
{
    public CharacterSkillSet mySkills;

    public void DefineSkills(CharacterSkillSet mySkills)
    {
        mySkills.primaryExecute = (manager) =>
        {
            bool rotDown = false;
            var rot = rotDown ? new Vector3(0, 0, 1f) : new Vector3(0, 0, -1f);

            if (!mySkills.primaryHasExecuted)
            {
                mySkills.primaryHasExecuted = true;
                mySkills.primaryOrigin.gameObject.SetActive(true);
                manager.ExecuteUntil(timeLimit: mySkills.primaryCooldown, () =>
                {
                    mySkills.primaryOrigin.Rotate(rot);

                    if (mySkills.primaryOrigin.rotation.eulerAngles.z >= 60) rotDown = true;
                    else if (mySkills.primaryOrigin.rotation.eulerAngles.z <= 0) rotDown = false;

                });

                manager.WaitAndThen(timeToWait: mySkills.primaryCooldown, () =>
                {
                    mySkills.primaryHasExecuted = false;
                    mySkills.primaryOrigin.gameObject.SetActive(false);
                },
                cancelCondition: () => false);

            }
            else
            {
                manager.SetColliderAction(mySkills, true, SkillSlot.primary);

                var projectile = (mySkills.primarySpawn) ? Instantiate(mySkills.primarySpawn,
                                                            mySkills.primaryOrigin.position,
                                                            mySkills.primaryOrigin.rotation)
                                                         :  default;


                if (projectile != default) projectile.GetComponent<Rigidbody>().
                                                      AddForce(mySkills.primaryOrigin.right 
                                                              * mySkills.primaryDistance);
                
                else Debug.Log("There is no projectile.");

                mySkills.primaryHasExecuted = false;
                mySkills.primaryOrigin.gameObject.SetActive(false);
            }

        };

        mySkills.secondaryExecute = (manager) =>
        {

        };
    }

    void Awake()
    {
        DefineSkills(mySkills);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
