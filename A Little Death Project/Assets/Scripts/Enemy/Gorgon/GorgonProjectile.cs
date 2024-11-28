using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorgonProjectile : MonoBehaviour
{
    [HideInInspector] public PlayerSkillManager skillManager;
    [HideInInspector] public Action OnImpact;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var interactable = collision.GetComponent<IInteractable>();

        if(interactable != null && skillManager.GetColliderAction() == ColliderAction.Break)
        {
            print("la bala colisiono");
            interactable.PerformInteraction();
            OnImpact();
        }
    }
}
