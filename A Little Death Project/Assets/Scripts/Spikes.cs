using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] ThaniaHealth player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == player.gameObject.GetComponent<Collider2D>())
        {
            player.Damage(1);
            StartCoroutine(player.Knockback(transform.position));
        }
    }
}
