using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    GameObject thania;

    private void Start()
    {
        thania = GameObject.FindWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Nos re vimos");
        if (collision.gameObject == thania) 
        {
            SceneManager.LoadScene("Victory");
        }
    }
}
