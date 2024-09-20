using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    [SerializeField] GameObject thania;

    private void Start()
    {
        thania = GameObject.FindWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == thania) 
        {
            Debug.Log("Nos re vimos");
            Checkpoints.savedPos = default;
            SceneManager.LoadScene("Victory");
        }
    }
}
