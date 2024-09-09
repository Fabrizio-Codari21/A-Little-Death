using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementParticleController : MonoBehaviour
{
    [SerializeField] ParticleSystem movementParticle;

    [Range(0, 10)][SerializeField] int occurAfterVelocity;

    [Range(0, 2f)][SerializeField] float dustFormationPeriod;

    [SerializeField] Rigidbody2D rb;

    [SerializeField] JumpManager jm;

    float counter;

    private void Update()
    {
        counter += Time.deltaTime;

        if(Mathf.Abs(rb.velocity.x)>occurAfterVelocity && jm.grounded)
        {
            if(counter > dustFormationPeriod) 
            {
                movementParticle.Play();
                counter = 0;
            }
        }
    }
}
