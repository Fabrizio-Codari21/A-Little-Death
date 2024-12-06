using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorgonProjectile : MonoBehaviour
{
    [HideInInspector] public PlayerSkillManager skillManager;
    [HideInInspector] public Action OnImpact;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        float angelRad = Mathf.Atan2(rb.velocity.x, rb.velocity.y);
        float angelDeg = (180/Mathf.PI) * angelRad - 90;

        transform.rotation = Quaternion.Euler(0,0,-angelDeg);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var interactable = collision.GetComponent<IInteractable>();

        if(interactable != null)
        {
            print("la bala colisiono");
            interactable.PerformInteraction(true);
            OnImpact();
        }
        else if(interactable != null)
        {
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Player")
        {
            collision.GetComponentInParent<ThaniaHealth>().Damage(gameObject, 1);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
