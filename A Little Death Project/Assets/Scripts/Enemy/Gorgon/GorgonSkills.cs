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
            var rot = rotDown ? new Vector3(0, 0, -1f) : new Vector3(0, 0, 1f);

            var dir = manager.thaniaMovement.isFacingRight
                      ? mySkills.primaryOrigin.right
                      : mySkills.primaryOrigin.right * -1;

            manager.thaniaMovement.rb.velocity = Vector2.zero;
            manager.thaniaMovement.canMove = false;

            if (!mySkills.primaryHasExecuted)
            {
                mySkills.primaryHasExecuted = true;
                mySkills.primaryOrigin.gameObject.SetActive(true);


                //manager.ExecuteUntil(timeLimit: mySkills.primaryCooldown, () =>
                //{
                //    if (mySkills.primaryOrigin.rotation.eulerAngles.z >= 60) rot = new Vector3(0, 0, -1f);
                //    else if (mySkills.primaryOrigin.rotation.eulerAngles.z <= 0) rot = new Vector3(0, 0, 1f);

                //    mySkills.primaryOrigin.Rotate(rot);

                //}, cancelCondition: () => !mySkills.primaryHasExecuted);

                manager.WaitAndThen(timeToWait: mySkills.primaryCooldown, () =>
                {
                    mySkills.primaryHasExecuted = false;
                    mySkills.primaryOrigin.gameObject.SetActive(false);
                    mySkills.primaryOrigin.rotation = Quaternion.identity;
                    manager.thaniaMovement.canMove = true;
                },
                cancelCondition: () => !mySkills.primaryHasExecuted);

            }
            else
            {
                manager.SetColliderAction(mySkills, true, SkillSlot.primary);

                var projectile = (mySkills.primarySpawn) ? Instantiate(mySkills.primarySpawn,
                                                            mySkills.primaryOrigin.position,
                                                            mySkills.primaryOrigin.rotation).
                                                            GetComponent<GorgonProjectile>()
                                                         :  default;

                if (projectile != default)
                { 
                    projectile.GetComponent<Rigidbody2D>().AddForce(dir * mySkills.primaryDistance * 100);
                    projectile.skillManager = manager;
                    projectile.OnImpact = () =>
                    {
                        manager.SetColliderAction(mySkills, false, SkillSlot.primary);
                        Destroy(projectile.gameObject, 0.02f);
                    };
                }
                else Debug.Log("There is no projectile.");

                mySkills.primaryHasExecuted = false;
                mySkills.primaryOrigin.gameObject.SetActive(false);
                mySkills.primaryOrigin.rotation = Quaternion.identity;
                manager.CanMove();
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
