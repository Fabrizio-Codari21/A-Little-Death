using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WingsAttackDetection : MonoBehaviour
{
    public Harpy harpy;
    private RaycastHit2D hits;
    [SerializeField] int damage = 2;
    public HarpyAnimController anim;
    private bool attacking;
    [SerializeField] private float nextFireTime;
    private bool attacked;
    Collider2D target;
    [SerializeField] private float cooldown;

    private void Awake()
    {
        harpy = GetComponentInParent<Harpy>();
    }

    private void Update()
    {
        if (attacking && harpy.canMove)
        {
            if (Time.time > nextFireTime && attacked == false)
            {
                attacked = true;
                anim.attacked = true;
                Attack(target);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            attacking = true;
            target = other;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            attacking = false;
            target = null;
        }
    }

    public void Attack(Collider2D collision)
    {
        StartCoroutine(ShowHitbox());
        var damageableObject = collision.gameObject.GetComponent<ThaniaHealth>();
        damageableObject.Damage(damage);
        nextFireTime = Time.time + cooldown;
    }

    private IEnumerator ShowHitbox()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        transform.GetChild(1).gameObject.SetActive(false);
        anim.attacked = false;
        attacked = false;
    }
}
