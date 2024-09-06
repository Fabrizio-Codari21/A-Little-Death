using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class USPTutorial : Health
{
    //public GameObject manager;
    //SkillSelector skillSelector;
    [SerializeField] int damage = 1;
    [SerializeField] ParticleSystem[] particleSystems;

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
        Destroy(gameObject);
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
