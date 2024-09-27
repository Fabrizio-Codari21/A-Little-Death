using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpy : RoamingMovement
{
    private GameObject thania;
    public bool detected = false;
    [SerializeField] Rigidbody2D rb;

    public float speedHunt = 7f;
    public float nextWaypointDistance = 3f;
    [SerializeField]Seeker seeker;
    Path path;
    int currentWaypoint = 0; 
    bool reachedEndOfPath = false;

    private void Start()
    {
        thania = GameObject.FindWithTag("Player");
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
        {
            seeker.StartPath(rb.position, thania.transform.position, OnPathComplete);
        }
    }

    private void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    public override void Update()
    {
        if (canMove)
        {
            if (detected == true)
            {
                Flip();
                pathfindingUpdate();
            }
            else
            {
                base.Update();
            }
        }
    }

    void pathfindingUpdate()
    {
        if(path == null)
            return;

        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speedHunt*Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;

        if (thania.transform.position.x > transform.position.x)
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
