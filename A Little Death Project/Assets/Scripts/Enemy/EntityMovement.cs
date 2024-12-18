using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : Spawnable
{
    public bool canMove = true;
    public bool facingRight;

    public void StopMoving()
    {
        canMove = false;
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        body.velocity = Vector2.zero;
    }
    
    private void OnDestroy()
    {
        OnDespawn();
    }

    private void OnDisable()
    {
        OnDespawn();
    }
}
