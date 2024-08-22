using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCRunner : MonoBehaviour
{
    public bool activated = false;
    public bool canRun = true;
    public Animator animator;
    private float nextFireTime;
    [SerializeField] private float cooldown;

    private void Update()
    {
        if (Time.time > nextFireTime)
        {
            canRun = true;
            nextFireTime = Time.time + cooldown;
        }

        animator.SetBool("Activated", activated);
        animator.SetBool("CanRun", canRun);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            activated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            activated = false;
            canRun = false;
        }
    }
}
