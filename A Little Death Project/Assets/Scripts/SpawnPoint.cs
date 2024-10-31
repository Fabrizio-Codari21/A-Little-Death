using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public EnemyManager enemyManager;
    
    //public GameObject spawnPoint;
    [HideInInspector] public bool hasAlreadySpawned = false;
    public GameObject entityToSpawn;
    public Vector2 spawnOffset;
    public AudioSource spawnSound;
    int spawnAmount;

    public PlayerSkillManager player;
    public float requiredPlayerDistance;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + new Vector3(spawnOffset.x, spawnOffset.y, 0), requiredPlayerDistance);
    }

    private void Awake()
    {
        entityToSpawn.GetComponentInChildren<Spawnable>().parentSpawner = this;
        
        if (!enemyManager) GameObject.FindAnyObjectByType<EnemyManager>();
        if (!player) player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkillManager>();
        Extensions.SpawnPoints.Add(this);
    }

    private void Update()
    {
        if (spawnAmount < 1)
        {
            entityToSpawn.SpawnAt(this);
            hasAlreadySpawned = true;
            spawnAmount++;
        }
        else if (!hasAlreadySpawned && Vector3.Distance(player.transform.position, transform.position) >= requiredPlayerDistance)
        {
            entityToSpawn.SpawnAt(this);
            hasAlreadySpawned = true;
        }
    }
}
