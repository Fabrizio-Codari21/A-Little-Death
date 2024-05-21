using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThaniaHealth : Health
{
    public float invulnerable;
    public float damageCooldown;

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
        transform.GetChild(4).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        transform.GetChild(4).gameObject.SetActive(false);
    }
}
