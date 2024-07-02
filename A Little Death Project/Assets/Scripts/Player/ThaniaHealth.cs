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

    public override void Start()
    {
        base.Start();
        sRenderer = transform.GetChild(5).gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public override void Damage(int damage)
    {
        if (Time.time > invulnerable)
        {
            invulnerable = Time.time + damageCooldown;
            StartCoroutine(takeDamage());
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public override void Die()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("Death");
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

            rb.velocity = new Vector2(knockbackPow, knockbackPow);
        }

        yield return 0;
    }
}
