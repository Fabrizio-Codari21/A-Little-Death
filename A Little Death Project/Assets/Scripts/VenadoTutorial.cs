using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenadoTutorial : Health
{
    public GameObject manager;
    SkillSelector skillSelector;
    [SerializeField] int damage = 1;
    [SerializeField] ParticleSystem[] particleSystems;

    public override void Start()
    {
        base.Start();
        manager = GameObject.FindWithTag("Manager");
        skillSelector = manager.GetComponent<SkillSelector>();
    }

    public override void Die()
    {
        skillSelector.selecting = true;
        skillSelector.enemyID = "tutorial";
        foreach (var p in particleSystems)
        {
            Instantiate(p, transform.position, Quaternion.Euler(270, 180, 0));
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            var damageableObject = collision.gameObject.GetComponent<ThaniaHealth>();
            damageableObject.Damage(damageableObject.gameObject, damage);
        }
    }
}
