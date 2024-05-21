using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilityUI : MonoBehaviour
{
    public void AlasDash()
    {
        Debug.Log("Alas en Dash");
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
    }
    
    public void FuerzaDash()
    {
        Debug.Log("Fuerza en Dash");
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    public void AlasAtaque()
    {
        Debug.Log("Alas en Ataque");
        transform.GetChild(2).gameObject.SetActive(true);
    }
}
