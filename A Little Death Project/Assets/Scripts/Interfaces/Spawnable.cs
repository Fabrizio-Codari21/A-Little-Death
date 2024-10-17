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
    }

    public void OnDespawn()
    {
        parentSpawner.hasAlreadySpawned = false;
    }
}
