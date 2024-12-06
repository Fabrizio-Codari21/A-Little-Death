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

    public GameObject timerSoul;
    //public Vector2 offset;

    //Vector3 originalScale;
    //Vector3 originalPos;

    //private void Start()
    //{
    //    if (timerSoul)
    //    {
    //        originalPos = timerSoul.transform.localPosition;
    //        originalScale = timerSoul.transform.localScale;
    //    }
    //}

    //private void Update()
    //{
    //    if (timerSoul)
    //    {
    //        timerSoul.transform.position = transform.position + new Vector3(offset.x, offset.y, 0);
    //    }
    //}
}
