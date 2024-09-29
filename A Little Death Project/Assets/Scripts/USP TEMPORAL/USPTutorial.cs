using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class USPTutorial : PossessableHealth
{
    [SerializeField] int damage = 1;
    [SerializeField] ParticleSystem[] particleSystems;
    public TutorialBox tutorial;

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
        if(tutorial != null) 
        {
            tutorial.textToPrint = "Con <b>'Click Derecho'</b> vas a poder usar la habilidad secundaria brindada por tu enemigo para desplazarte.";
            tutorial.gameObject.SetActive(true);
        }
        gameObject.SetActive(false);
        Destroy(gameObject, 5f);
    }

    private void OnDestroy()
    {
        tutorial.movingBack = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            var damageableObject = collision.gameObject.GetComponent<ThaniaHealth>();
            damageableObject.Damage(damage);
        }
    }
}
