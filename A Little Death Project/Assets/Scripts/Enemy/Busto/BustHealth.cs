using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BustHealth : PossessableHealth
{
    [SerializeField] int damage = 1;
    [SerializeField] ParticleSystem[] particleSystems;
    //public TutorialColliders tutorial;
    //public TutorialColliders tutorialVenado;
    [SerializeField] FreeRoamMovement movementManager;

    public override void Start()
    {
        base.Start();

        movementManager = GetComponent<FreeRoamMovement>();
    }

    public override void Die()
    {
        foreach (var p in particleSystems)
        {
            Instantiate(p, transform.position, Quaternion.Euler(270, 180, 0));
        }

        base.Die();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && movementManager.canMove == true)
        {
            Debug.Log(movementManager.canMove);
            var damageableObject = collision.gameObject.GetComponent<ThaniaHealth>();
            if(!damageableObject.immune) damageableObject.Damage(gameObject, damage);
        }
    }
}
