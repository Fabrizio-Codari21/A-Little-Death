using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class WingsAttack : AttackList
{
    [SerializeField] private Transform attackTransform;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask attackableLayer;
    [SerializeField] int damage = 1;

    private RaycastHit2D hits;

    private void Awake()
    {
        classType = "alas";
    }

    private void Update()
    {
        if (attacked == true)
        {
            hits = Physics2D.CircleCast(attackTransform.position, attackRange, transform.right, 0f, attackableLayer);

            if (hits != false)
            {
                Health damageable = hits.collider.gameObject.GetComponent<Health>();

                if (damageable != null)
                {
                    damageable.Damage(GetComponentInParent<GameObject>(), damage);
                }
            }
        }
    }

    public override void Attack()
    {
        if (Time.time > nextFireTime)
        {
            attacked = true;
            StartCoroutine(ShowHitbox());
            nextFireTime = Time.time + cooldown;
        }
    }

    private IEnumerator ShowHitbox()
    {
        transform.GetChild(3).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        attacked = false;
        transform.GetChild(4).gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackTransform.position, attackRange);
    }
}
