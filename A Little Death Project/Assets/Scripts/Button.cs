using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject itemToTrigger;
    bool activated = false;

    public void PerformInteraction(bool activate)
    {
        //animacion de que aparezca un puente, se abra una puerta, etc.
        if(itemToTrigger) itemToTrigger.SetActive(activate);
        transform.position += new Vector3(0, 0.25f * (activate ? -1 : 1), 0);
        activated = activate;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Interactable")//|| collision.gameObject.tag == "Player"))
        {
            PerformInteraction(!activated);
        }
    }
}
