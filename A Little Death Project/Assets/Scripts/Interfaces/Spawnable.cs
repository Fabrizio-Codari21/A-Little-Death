using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Spawnable : MonoBehaviour
{
    [HideInInspector] public SpawnPoint parentSpawner;

    public void OnSpawn()
    {
        if(!parentSpawner) parentSpawner = this.GetNearest(gameObject, Extensions.SpawnPoints);
        parentSpawner.hasAlreadySpawned = true;

        var health = GetComponentInChildren<PossessableHealth>();

        health.startedPossession = false;

        //health.canBePossessed = true;

        health.ResetHealth();

        if (GetComponent<Harpy>()) { GetComponent<Rigidbody2D>().gravityScale = 0; }

        GetComponent<EntityMovement>().canMove = true;
        if (GetComponent<Light2D>()) { GetComponent<Light2D>().enabled = true; }
    }

    public void OnDespawn()
    {
        if(parentSpawner) parentSpawner.hasAlreadySpawned = false;
    }
}
