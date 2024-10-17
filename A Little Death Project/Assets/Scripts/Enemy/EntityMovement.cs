using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class EntityMovement : Spawnable
{
    [HideInInspector] public bool canMove = true;

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
