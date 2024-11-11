using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpyDetection : MonoBehaviour
{
    public Harpy harpy;
    [SerializeField] bool onEnter = true;

    private void Awake()
    {
        harpy = GetComponentInParent<Harpy>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (onEnter)
        {
            if (other.transform.tag == "Player")
            {
                harpy.detected = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!onEnter)
        {
            if (other.transform.tag == "Player")
            {
                harpy.detected = false;
            }
        }
    }
}

