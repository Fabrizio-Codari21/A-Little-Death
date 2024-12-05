using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilityUI : MonoBehaviour
{
    public void AlasDash()
    {
        //Debug.Log("Alas en Dash");
        transform.GetChild(5).gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
    }
    
    public void FuerzaDash()
    {
        //Debug.Log("Fuerza en Dash");
        transform.GetChild(5).gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    public void AlasAtaque()
    {
        //Debug.Log("Alas en Ataque");
        transform.GetChild(5).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(true);
    }

    public void Default()
    {
        //Debug.Log("Nada en Dash");
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(false);
        transform.GetChild(5).gameObject.SetActive(true);
    }
    
    public void Busto()
    {
        //Debug.Log("Nada en Dash");
        transform.GetChild(5).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(true);
        transform.GetChild(4).gameObject.SetActive(true);
    }
    
    public void Gorgona()
    {
        //Debug.Log("Nada en Dash");
        transform.GetChild(5).gameObject.SetActive(false);
        transform.GetChild(6).gameObject.SetActive(true);
    }
}
