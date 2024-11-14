using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableFloor : MonoBehaviour, IBreakable
{
    public Rigidbody2D rb;
    public Sprite brokenSprite;
    [SerializeField] ParticleSystem particleSystems;

    public void Break(GameObject breaker)
    {
        if(particleSystems) Instantiate(particleSystems, transform.position, Quaternion.Euler(270, 0, 0));
        Destroy(gameObject);
    }


}
