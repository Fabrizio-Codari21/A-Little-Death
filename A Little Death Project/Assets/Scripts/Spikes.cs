using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] ThaniaHealth player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            player.Damage(gameObject, 1);
           StartCoroutine(player.Knockback(transform.position));
        }
    }
}
