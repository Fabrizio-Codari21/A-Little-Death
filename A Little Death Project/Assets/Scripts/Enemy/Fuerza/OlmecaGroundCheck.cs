using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OlmecaGroundCheck : MonoBehaviour
{
    [SerializeField] OlmecaAttack manager;

    private void Start()
    {
        manager = GetComponentInParent<OlmecaAttack>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Platform")
        {
            manager.anim.landed = true;
        }
    }
}
