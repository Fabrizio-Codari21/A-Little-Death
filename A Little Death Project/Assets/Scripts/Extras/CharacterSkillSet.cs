using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Nos permite asignar las habilidades de cada personaje desde el inspector.
// (ESTE ES EL SCRIPT QUE HAY QUE ARRASTRAR PARA EL PLAYER Y CADA ENEMIGO POSEIBLE)
public class CharacterSkillSet : MonoBehaviour
{
    [Header("CHARACTER INFO")]
    [Tooltip("What does this creature look like? (and what should the player look like when possessing them)")]
    public PlayerAppearance creatureAppearance;
    public bool previewFeetAreaSize;
    [Tooltip("The size of the hitbox this character uses when checking whether it's on the ground.")]
    public Vector2 creatureFeetArea;

    [Header("PRIMARY SKILL")]
    public SkillType primarySkillType;
    [Tooltip("Represents distance values of your current action, such as range, radius, etc.")]
    public float primaryDistance;
    [Tooltip("Represents the amount of something that your action applies, such as damage, points, etc.")]
    public float primaryEffectAmount;
    [Tooltip("Represents the layer that your action may affect.")]
    public LayerMask primaryValidLayer;
    [Tooltip("Represents the type of action that will be performed upon colliding with certain objects.")]
    public ColliderAction primaryColliderAction;
    [Tooltip("Represents the origin point of your action.")]
    public Transform primaryOrigin;
    [Tooltip("Represents an object that will be spawned when the action is performed.")]
    public GameObject primarySpawn;
    [Tooltip("Represents the time it takes before your action can be executed again.")]
    public float primaryCooldown;
    
    [HideInInspector] public float primaryExecTime;
    [HideInInspector] public bool primaryHasExecuted;
    [HideInInspector] public bool primary2HasExecuted;
    public Action<PlayerSkillManager> primaryExecute;
    
    [Space(20), Header("SECONDARY SKILL")]   
    public SkillType secondarySkillType;
    [Tooltip("Represents distance values of your current action, such as range, radius, etc.")]
    public float secondaryDistance;
    [Tooltip("Represents the amount of something that your action applies, such as damage, points, etc.")]
    public float secondaryEffectAmount;
    [Tooltip("Represents the layer that your action may affect.")]
    public LayerMask secondaryValidLayer;
    [Tooltip("Represents the type of action that will be performed upon colliding with certain objects.")]
    public ColliderAction secondaryColliderAction;
    [Tooltip("Represents the origin point of your action.")]
    public Transform secondaryOrigin;
    [Tooltip("Represents an object that will be spawned when the action is performed.")]
    public GameObject secondarySpawn;
    [Tooltip("Represents the time it takes before your action can be executed again.")]
    public float secondaryCooldown;

    [HideInInspector] public float secondaryExecTime;
    [HideInInspector] public bool secondaryHasExecuted;
    // Action<GameObject> o lo que haga falta
    public Action<PlayerSkillManager> secondaryExecute;

    // En estos structs se guarda el set de habilidades para que el jugador pueda acceder
    // a ellas facilmente.
    public SkillSet primarySkill = new();
    public SkillSet secondarySkill = new();

    private void Awake()
    {     
        primarySkill = new SkillSet
        {
            skillType = primarySkillType,
            skillSlot = SkillSlot.primary,
            distance = primaryDistance,
            effectAmount = primaryEffectAmount,
            validLayer = primaryValidLayer,
            colliderAction = primaryColliderAction,
            origin = primaryOrigin,
            spawn = primarySpawn,
            cooldown = primaryCooldown,
            nextFireTime = primaryExecTime,
            hasExecuted = primaryHasExecuted,
            Execute = primaryExecute,
        };

        secondarySkill = new SkillSet
        {
            skillType = secondarySkillType,
            skillSlot = SkillSlot.secondary,
            distance = secondaryDistance,
            effectAmount = secondaryEffectAmount,
            validLayer = secondaryValidLayer,
            colliderAction = secondaryColliderAction,
            origin = secondaryOrigin,
            spawn = secondarySpawn,
            cooldown = secondaryCooldown,
            nextFireTime = secondaryExecTime,
            hasExecuted = secondaryHasExecuted,
            Execute = secondaryExecute,
        };

        Debug.Log($"{gameObject.name} has spawned.");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        if(previewFeetAreaSize) Gizmos.DrawWireCube(transform.position, new Vector3(creatureFeetArea.x, creatureFeetArea.y, 0));
    }
}
