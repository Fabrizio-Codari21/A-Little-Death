using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorgonProjectile : MonoBehaviour
{
    [HideInInspector] public PlayerSkillManager skillManager;
    [HideInInspector] public Action OnImpact;

    private void OnTriggerStay2D(Collider2D collision)
    {
        var interactable = collision.GetComponent<IInteractable>();

        if(interactable != null && skillManager.GetColliderAction() == ColliderAction.Break)
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
