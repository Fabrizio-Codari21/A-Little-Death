using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] 
    JumpManager manager;
    [SerializeField] Vector2 feet;

    private void Start()
    {
        manager = GetComponentInParent<JumpManager>();
    }

    private void Update()
    {
        manager.grounded = Physics2D.OverlapBox(transform.position, feet, 0, manager.groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, feet);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Platform")
        {
            manager.anim.jumped = false;
        }
    }
}
