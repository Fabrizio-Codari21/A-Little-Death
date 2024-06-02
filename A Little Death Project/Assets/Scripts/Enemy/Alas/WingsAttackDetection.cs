using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsAttackDetection : MonoBehaviour
{
    public Harpy harpy;
    private RaycastHit2D hits;
    [SerializeField] int damage = 2;

    private void Awake()
    {
        harpy = GetComponentInParent<Harpy>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            Attack(other);
        }
    }

    public void Attack(Collider2D collision)
    {
        harpy.detected = false;
        StartCoroutine(ShowHitbox());
        var damageableObject = collision.gameObject.GetComponent<ThaniaHealth>();
        damageableObject.Damage(damage);
    }

    private IEnumerator ShowHitbox()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        transform.GetChild(1).gameObject.SetActive(false);
    }
}