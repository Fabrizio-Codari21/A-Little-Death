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

    public override void Start()
    {
        base.Start();
        sRenderer = transform.GetChild(5).gameObject.GetComponent<SpriteRenderer>();
    }

    public override void Damage(float damage)
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
        //transform.GetChild(4).gameObject.SetActive(true);
        sRenderer.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        sRenderer.color = Color.white;
        //transform.GetChild(4).gameObject.SetActive(false);
    }
}
