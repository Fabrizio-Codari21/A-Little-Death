using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    [SerializeField] GameObject fade;
    public string level;

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") 
        {
            other.GetComponentInParent<ThaniaMovement>().stop();
            Checkpoints.active = false;
            Checkpoints.savedPos = default;
            fade.SetActive(true);
            Debug.Log("Nos re vimos");
            this.WaitAndThen(1.5f, () => { this.AsyncLoader(level); });   
        }
    }
}
