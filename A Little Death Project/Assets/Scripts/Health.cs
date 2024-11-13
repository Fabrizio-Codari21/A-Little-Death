using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] protected int maxHealth = 1;

    public int currentHealth;
    public AudioManager audioManager;

    public virtual bool Damage(GameObject damager, int damage)
    {
        Debug.Log(this + "took damage");
        currentHealth -= damage;
        KnockBack(damager, damage);
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
        if (!audioManager) audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    public virtual void Die()
    {
        Destroy(gameObject, 0.05f);
    }

    public virtual void KnockBack(GameObject damager, int power, bool returnMovement = true)
    {
        Debug.Log(this + " was knocked back.");
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            float forceX = 200;
            float forceY = 66;

            rb.velocity = Vector2.zero;

            var move = GetComponent<EntityMovement>();
            if (move != null) 
            {
                move.canMove = false;
                if (move is ThaniaMovement)
                {
                    forceX /= 10;
                    forceY /= 2;                 
                }
                if (move is Harpy)
                {
                    forceX /= 20;
                    forceY /= 5;
                    rb.gravityScale = 20;
                }

                if (damager.transform.position.x > transform.position.x)
                {
                    forceX *= -1;
                }
            }

            

            //rb.velocity += (new Vector2(power * 10000, power * 10));
            //rb.AddForce(new Vector2(power * 10000, power * 300));


            this.ExecuteUntil(0.25f, () =>
            {
                rb.AddForce(new Vector3(power * forceX, power * forceY, 0));
                forceX *= 0.99f;
                forceY *= 0.93f;
            });

            this.WaitAndThen(0.4f, () =>
            {
                if (move != null && returnMovement == true) move.canMove = true;
            });
        }        
        else this.ExecuteUntil(timeLimit: 0.5f, () =>
        {
            transform.position += new Vector3(power * 0.05f, power * 0.02f, 0);
        });

        
    }
}
