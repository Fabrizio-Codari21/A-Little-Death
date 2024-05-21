using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthDash : DashList
{
    bool canDash = true;
    [SerializeField] float dashingPower = 24f;
    [SerializeField] float dashingtime = 0.2f;
    [SerializeField] float dashingCooldown = 1f;
    private Rigidbody2D rb;
    public ThaniaMovement thania;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        thania = GetComponent<ThaniaMovement>();
        classType = "fuerza";
    }

    public override void Dash()
    {
        if(canDash == true)
        {
            StartCoroutine(DoDash());
        }
    }

    IEnumerator DoDash()
    {
        canDash = false;
        thania.isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingtime);
        rb.gravityScale = originalGravity;
        thania.isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
