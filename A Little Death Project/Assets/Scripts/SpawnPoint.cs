using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    //public GameObject spawnPoint;
    [HideInInspector] public bool hasAlreadySpawned;
    public GameObject entityToSpawn;
    public Vector2 spawnOffset;
    public AudioSource spawnSound;

    public PlayerSkillManager player;
    public float requiredPlayerDistance;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + new Vector3(spawnOffset.x, spawnOffset.y, 0), requiredPlayerDistance);
    }

    private void Awake()
    {
        if(!player) player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkillManager>();
        Extensions.SpawnPoints.Add(this);
    }

    private void Update()
    {
        if (!hasAlreadySpawned && Vector3.Distance(player.transform.position, transform.position) >= requiredPlayerDistance)
        {
            entityToSpawn.SpawnAt(this);
            hasAlreadySpawned = true;
        }
    }
}
