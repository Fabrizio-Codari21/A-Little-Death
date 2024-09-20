using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class USPTutorial : PossessableHealth
{
    //public GameObject manager;
    //SkillSelector skillSelector;
    [SerializeField] int damage = 1;
    [SerializeField] ParticleSystem[] particleSystems;
    public TutorialColliders tutorial;

    public override void Start()
    {
        base.Start();
    }

    public override void Die()
    {
        foreach (var p in particleSystems)
        {
            Instantiate(p, transform.position, Quaternion.Euler(270, 180, 0));
        }
        tutorial.ActivateTutorial(true);
        gameObject.SetActive(false);
        Destroy(gameObject, 5f);
        Destroy(tutorial.gameObject, 5f);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            var damageableObject = collision.gameObject.GetComponent<ThaniaHealth>();
            damageableObject.Damage(damage);
        }
    }

    private void OnDestroy()
    {
        tutorial.ActivateTutorial(false);
    }
}
