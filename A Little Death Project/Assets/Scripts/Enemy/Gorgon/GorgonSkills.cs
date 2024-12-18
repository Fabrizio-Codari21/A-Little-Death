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
                manager.thaniaMovement.anim.animator.SetTrigger("Aim");
                //mySkills.primaryOrigin.gameObject.SetActive(true);


                //manager.ExecuteUntil(timeLimit: mySkills.primaryCooldown, () =>
                //{
                //    if (mySkills.primaryOrigin.rotation.eulerAngles.z >= 60) rot = new Vector3(0, 0, -1f);
                //    else if (mySkills.primaryOrigin.rotation.eulerAngles.z <= 0) rot = new Vector3(0, 0, 1f);

                //    mySkills.primaryOrigin.Rotate(rot);

                //}, cancelCondition: () => !mySkills.primaryHasExecuted);

                var pivot = mySkills.primaryOrigin.GetComponent<GorgonPivot>();

                manager.ExecuteAfterTrue(() => (pivot.counter >= 3 && mySkills.primaryHasExecuted), () =>
                {
                    manager.thaniaMovement.anim.animator.SetTrigger("Cancel"); 
                    mySkills.primaryHasExecuted = false;
                    //mySkills.primaryOrigin.rotation = Quaternion.identity;
                    manager.thaniaMovement.canMove = true;
                    pivot.counter = 0;
                });
               
                //manager.WaitAndThen(timeToWait: mySkills.primaryCooldown, () =>
                //{
                //    mySkills.primaryHasExecuted = false;
                //    mySkills.primaryOrigin.gameObject.SetActive(false);
                //    mySkills.primaryOrigin.rotation = Quaternion.identity;
                //    manager.thaniaMovement.canMove = true;
                //},
                //cancelCondition: () => !mySkills.primaryHasExecuted);

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
                    manager.thaniaMovement.anim.animator.SetTrigger("Spit");

                    /*if (manager.thaniaMovement.isFacingRight)
                    {
                        projectile.GetComponent<SpriteRenderer>().flipX = false;
                    }
                    else
                    {
                        projectile.GetComponent<SpriteRenderer>().flipX = true; 
                    }*/
                    manager.thaniaHealth.audioManager.spit.Play();
                    projectile.GetComponent<Rigidbody2D>().AddForceAtPosition(dir * mySkills.primaryDistance * 100, mySkills.primaryOrigin.position);
                    projectile.skillManager = manager;
                    projectile.OnImpact = () =>
                    {
                        manager.SetColliderAction(mySkills, false, SkillSlot.primary);
                        Destroy(projectile.gameObject, 0.02f);
                    };
                }
                else Debug.Log("There is no projectile.");

                mySkills.primaryHasExecuted = false;
                //mySkills.primaryOrigin.gameObject.SetActive(false);
                //mySkills.primaryOrigin.rotation = Quaternion.identity;
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
