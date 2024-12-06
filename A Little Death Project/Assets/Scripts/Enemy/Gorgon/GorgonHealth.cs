using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GorgonHealth : PossessableHealth
{
    [SerializeField] int damage = 1;
    [SerializeField] ParticleSystem[] particleSystems;
    [SerializeField] FreeRoamMovement movementManager;

    public override void Start()
    {
        base.Start();

        movementManager = GetComponent<FreeRoamMovement>();
    }

    public override bool Damage(GameObject damager, int damage)
    {
        Debug.Log(this + "took damage");
        currentHealth -= damage;
        KnockBack(damager, damage, currentHealth <= 0 ? false : true);
        if(movementManager is GorgonMovement && currentHealth >= 1) { GetComponent<GorgonMovement>().animator.SetTrigger("Hit"); }

        if (currentHealth <= 0)
        {
            if (audioManager) audioManager.stunned.Play();
            _possessable = Possessable();
            StartCoroutine(_possessable);
            return true;
        }
        else return false;
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
            if (!damageableObject.immune) damageableObject.Damage(gameObject, damage);
        }
    }
}
