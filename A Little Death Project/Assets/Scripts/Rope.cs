using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour, IInteractable
{
    public Rigidbody2D counterweight;

    public void PerformInteraction()
    {        
        gameObject.SetActive(false); // esto se reemplaza con animacion de romper la soga
        if (counterweight)
        {
            counterweight.gameObject.layer = LayerMask.NameToLayer("Interactuable");
            counterweight.simulated = true;
            Destroy(counterweight.gameObject, 1.5f);
        }
    }

}
