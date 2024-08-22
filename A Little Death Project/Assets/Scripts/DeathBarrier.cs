using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBarrier : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            var damageableObject = other.gameObject.GetComponent<ThaniaHealth>();
            damageableObject.Damage(100);
        }
        else
        {
            Debug.Log("UwU");
            Destroy(other.gameObject);
        }
    }
}
