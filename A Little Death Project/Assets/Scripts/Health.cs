using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 1;

    public int currentHealth;

    public virtual bool Damage(int damage)
    {
        Debug.Log(this + "took damage");
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
            return true;
        }
        else return false;
    }

    public virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void Die()
    {
        Destroy(gameObject, 0.05f);
    }
}
