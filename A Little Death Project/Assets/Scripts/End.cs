using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : Menu
{
    //[SerializeField] GameObject fade;
    public string level;

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") 
        {
            other.GetComponentInParent<ThaniaMovement>().StopMoving();
            other.GetComponentInParent<JumpManager>().StopMoving();

            Checkpoints.savedPos = default;

            StartCoroutine(waitForTransition(level));

        }
    }

}
