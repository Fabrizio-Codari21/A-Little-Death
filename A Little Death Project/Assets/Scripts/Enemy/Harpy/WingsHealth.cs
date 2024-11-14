using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsHealth : PossessableHealth
{
    public GameObject manager;
    SkillSelector skillSelector;
    [SerializeField] int damage = 1;
    [SerializeField] ParticleSystem[] particleSystems;
    [SerializeField] FreeRoamMovement movementManager;

    public override void Start()
    {
        base.Start(); 
        manager = GameObject.FindWithTag("Manager");
        skillSelector = manager.GetComponent<SkillSelector>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && movementManager == true)
        {
            var damageableObject = collision.gameObject.GetComponent<ThaniaHealth>();
            if (!damageableObject.immune) damageableObject.Damage(damageableObject.gameObject, damage);
        }
    }

    public override void Update()
    {
        if (canBePossessed && this.Inputs(MyInputs.Possess))
        {
            base.Update();
            _skillManager.jumpManager.dJump = true;
        }
    }
}
