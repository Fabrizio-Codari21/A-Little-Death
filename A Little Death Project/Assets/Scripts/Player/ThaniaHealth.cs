using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThaniaHealth : Health
{
    public float invulnerable;
    public float damageCooldown;
    public SpriteRenderer sRenderer;
    public Rigidbody2D rb;
    [SerializeField] float knockDur;
    [SerializeField] float knockbackPow;
    [SerializeField] AudioSource damageSound;
    [SerializeField] ThaniaMovement movementManager;

    [SerializeField] Menu menu;

    public override void Start()
    {
        base.Start();
        rb = gameObject.GetComponent<Rigidbody2D>();

        if (Checkpoints.savedPos != default)
        { 
            transform.position = Checkpoints.savedPos; 
        }
        else Checkpoints.savedPos = transform.position;
    }

    public override bool Damage(GameObject damager, int damage)
    {
        if (Time.time > invulnerable)
        {
            invulnerable = Time.time + damageCooldown;
            StartCoroutine(takeDamage());
            currentHealth -= damage;
            if(damager.tag != "Spikes")
            {
                Debug.Log(damager);
                KnockBack(damager, damage);
            }
            if (currentHealth <= 0)
            {
                Die();
                return true;
            }
        }
        return false;
    }

    public override void Die()
    {
        menu.Death();
        //gameObject.SetActive(false);
        movementManager.isDashing = true;
        movementManager.anim.animator.SetTrigger("Death");
    }

    IEnumerator takeDamage()
    {
        sRenderer.color = Color.red;
        damageSound.Play();
        yield return new WaitForSeconds(0.3f);
        sRenderer.color = Color.white;
    }

    public IEnumerator Knockback(Vector3 knockbackDir)
    {
        float timer = 0;

        while(knockDur > timer)
        {
            timer += Time.deltaTime;

            rb.velocity = new Vector2(0, knockbackPow*3);
        }

        yield return 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Checkpoint")
        {
            Checkpoints.savedPos = collision.gameObject.transform.position - Vector3.one;
            Checkpoints.active = true;
        }
    }
}
