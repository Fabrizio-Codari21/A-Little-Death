using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnable : MonoBehaviour
{
    [HideInInspector] public SpawnPoint parentSpawner;

    public void OnSpawn()
    {
        if(!parentSpawner) parentSpawner = this.GetNearest(gameObject, Extensions.SpawnPoints);
        parentSpawner.hasAlreadySpawned = true;

        var health = GetComponent<PossessableHealth>();

        health.startedPossession = false;

        health.canBePossessed = true;

        health.ResetHealth();

        if (GetComponent<Harpy>()) { GetComponent<Rigidbody2D>().gravityScale = 0; }

        GetComponent<EntityMovement>().canMove = true;
    }

    public void OnDespawn()
    {
        if(parentSpawner) parentSpawner.hasAlreadySpawned = false;
    }
}
