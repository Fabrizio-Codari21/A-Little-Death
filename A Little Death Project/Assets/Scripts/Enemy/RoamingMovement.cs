using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class RoamingMovement : EnemyMovement
{
    public List<GameObject> Waypoints;
    public float Speed = 5f;
    int index = 0;
    public static bool yendo = true;

    public virtual void Update()
    {
        if(canMove) Roaming();
    }

    public void Roaming()
    {
        Vector2 destination = Waypoints[index].transform.position;
        Vector2 newPos = Vector2.MoveTowards(transform.position, Waypoints[index].transform.position, Speed * Time.deltaTime);
        transform.position = newPos;
        float distance = Vector2.Distance(transform.position, destination);

        Flip(destination);
        if (distance <= 0.1)
        {
            if (index < Waypoints.Count - 1)
            {
                index++;
            }
            else
            {
                index = 0;
            }
        }
    }

    private void Flip(Vector2 destination)
    {
        Vector3 scale = transform.localScale;

        if (destination.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1;
        }
        else
        {
            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale;
    }
}

