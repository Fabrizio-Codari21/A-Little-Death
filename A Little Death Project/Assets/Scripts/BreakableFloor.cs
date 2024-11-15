using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableFloor : MonoBehaviour, IBreakable
{
    public Rigidbody2D rb;
    public Sprite brokenSprite;
    [SerializeField] ParticleSystem particleSystems;

    public void Break(GameObject breaker)
    {
        if(particleSystems) Instantiate(particleSystems, transform.position, Quaternion.Euler(270, 0, 0));
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        var manager = collision.gameObject.GetComponent<PlayerSkillManager>();

        if (manager != null && manager.GetColliderAction() == ColliderAction.Break && manager.isBreaking)
        {
            manager.jumpManager.anim.animator.SetTrigger("Stun");
            Break(manager.gameObject);
            Debug.Log($"{gameObject.name} was broken by {manager.sk.skills[1].skillType}");
        }

    }


}
