using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Cada GameObject que contenga uno de los sprites posibles del player deberia tener este script
// con su enum asignado (a medida que se agreguen sprites se los puede sumar a PlayerAppearance en ENUMS).
public class PlayerSprite : MonoBehaviour
{
    [Header("Please specify which entity")]
    public PlayerAppearance whatSpriteIsThis;
    public AnimationManager animator;
    public ParticleSystem soul;

    [HideInInspector] public ColliderAction actionWhenColliding;

    public Collider2D normalCollider;
    public Collider2D altCollider;
}
