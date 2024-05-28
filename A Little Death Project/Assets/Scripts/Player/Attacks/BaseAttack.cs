using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttack : AttackList
{
    [SerializeField] private Transform attackTransform;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask attackableLayer;
    [SerializeField] int damage = 2;
    public AnimationManager anim;

    private RaycastHit2D hits;

    private void Awake()
    {
        classType = "base";
    }

    private void Update()
    {
        if (attacked == true)
        {
            hits = Physics2D.CircleCast(attackTransform.position, attackRange, transform.right, 0f, attackableLayer);

            if (hits != false)
            {
                Debug.Log("Dealt Damage");

                IDamageable damageable = hits.collider.gameObject.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    damageable.Damage(damage);
                }
            }
        }
    }

    public override void Attack()
    {
        if (Time.time > nextFireTime && attacked == false)
        {
            attacked = true;
            Debug.Log("Attacked");
            anim.attacked = true;
            StartCoroutine(ShowHitbox());
            nextFireTime = Time.time + cooldown;
        }
    }

    private IEnumerator ShowHitbox()
    {
        transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        attacked = false;
        anim.attacked = false;
        transform.GetChild(2).gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackTransform.position, attackRange);
    }
}
