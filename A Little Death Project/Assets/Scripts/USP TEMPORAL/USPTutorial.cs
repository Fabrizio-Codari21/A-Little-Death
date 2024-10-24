using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class USPTutorial : PossessableHealth
{
    [SerializeField] int damage = 1;
    [SerializeField] ParticleSystem[] particleSystems;
    //public TutorialColliders tutorial;
    //public TutorialColliders tutorialVenado;
    public bool isTutorial;
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
        gameObject.SetActive(false);
        Destroy(gameObject, 5f);
    }

    private void OnPossess()
    {
        if(isTutorial) TutorialManager.instance.ChangeTutorial(1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && movementManager.canMove == true)
        {
            Debug.Log(movementManager.canMove);
            var damageableObject = collision.gameObject.GetComponent<ThaniaHealth>();
            damageableObject.Damage(gameObject, damage);
        }
    }

    public override void Update()
    {
        if (canBePossessed && this.Inputs(MyInputs.Possess))
        {
            base.Update();
            OnPossess();
        }
    }
}
