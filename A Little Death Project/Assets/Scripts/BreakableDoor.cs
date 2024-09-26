using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableDoor : MonoBehaviour, IBreakable
{
    public Rigidbody2D rb;
    public Sprite brokenSprite;

    public void Break(GameObject breaker)
    {
        // Aca iria la animacion de destruccion en vez de esto del rigidbody (o ambas)
        GetComponent<SpriteRenderer>().sprite = brokenSprite;

        rb.mass = 1000;
        rb.AddTorque(3600000);
        GetComponent<Collider2D>().isTrigger = true;
        rb.AddForceAtPosition(transform.up * 300000, breaker.transform.position);
        rb.AddForceAtPosition(breaker.transform.right * 1000000, breaker.transform.position);          
        Destroy(gameObject, 0.5f);
        
    }

    public void Start()
    {
        rb.mass = 10000000000;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var manager = collision.gameObject.GetComponent<PlayerSkillManager>();
        

        if (manager != null && manager.GetColliderAction() == ColliderAction.Break) 
        {
            Break(manager.gameObject);
            Debug.Log($"{gameObject.name} was broken by {manager.sk.skills[1].skillType}");
        }
        
    }

}
