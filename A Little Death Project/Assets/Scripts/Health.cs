using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 1;

    public int currentHealth;

    public virtual void Damage(int damage)
    {
        Debug.Log(this + "took damage");
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
